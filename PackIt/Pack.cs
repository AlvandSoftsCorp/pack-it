using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PackIt
{
    public class Pack
    {
        public delegate void PackInfo(string Info);
        public event PackInfo OnInfo = null;

        public Pack()
        {
            MyCypher mc = new MyCypher("123456789", this.GetRandomStr(10));
            mc.Cipher(ref ShredArray);
        }

        public string[] GetAllDirs(string DirPath)
        {
            string dir = DirPath.Trim();
            List<string> all_dirs = new List<string>();
            Stack<string> stack_dir = new Stack<string>();
            if (Directory.Exists(dir) == false) return null;
            stack_dir.Push(dir);
            while (stack_dir.Count != 0)
            {
                dir = stack_dir.Pop();
                all_dirs.Add(dir);
                string[] sub_dirs = Directory.GetDirectories(dir);
                foreach (string sub_dir in sub_dirs)
                {
                    stack_dir.Push(sub_dir);
                }
            }
            return all_dirs.ToArray();
        }
        
        public string[] GetAllFiles(string DirPath)
        {
            string dir = DirPath.Trim();
            List<string> all_files = new List<string>();
            Stack<string> stack_dir = new Stack<string>();
            if (Directory.Exists(dir) == false) return null;
            stack_dir.Push(dir);
            while (stack_dir.Count != 0)
            {
                dir = stack_dir.Pop();
                all_files.AddRange(Directory.GetFiles(dir));
                string[] sub_dirs = Directory.GetDirectories(dir);
                foreach (string sub_dir in sub_dirs)
                {
                    stack_dir.Push(sub_dir);
                }
            }
            return all_files.ToArray();
        }

        string GetRelPath(string AbsolutePath, string Root)
        {
            if (AbsolutePath == Root) return "/";
            string res = AbsolutePath.Replace(Root, "");
            res = res.Replace('\\', '/');
            return res;
        }

        public KeyValPair ProvideHeader(string DirPath)
        {
            if (this.OnInfo != null) this.OnInfo("Providing header...\r\n");
            
            // if given ource is a single file
            if (File.Exists(DirPath))
            {
                string file_name = DirPath;
                Prompt(string.Format("Adding File: {0}", file_name));
                if (this.OnInfo != null) this.OnInfo("Providing header...\r\n");
                FileInfo fi = new FileInfo(file_name);
                KeyValPair kvp = new KeyValPair(';',':');
                kvp.SetVal("D.CNT", "0");
                kvp.SetVal("F.CNT", "1");
                kvp.SetVal(string.Format("F.{0}", 0), fi.Name);
                kvp.SetVal(string.Format("F.{0}.O", 0), "0");
                kvp.SetVal(string.Format("F.{0}.L", 0), fi.Length.ToString());
                Prompt("--------------------------------");
                return kvp;
            }
            else
            {
                string dir = DirPath.Trim();
                string root = dir;

                if (Directory.Exists(dir) == false) return null;
                KeyValPair kvp = new KeyValPair(';',':');

                string[] all_dirs = this.GetAllDirs(dir);
                string[] all_files = this.GetAllFiles(dir);
                int dir_cnt = all_dirs.Length;
                int file_cnt = all_files.Length;


                kvp.SetVal("D.CNT", dir_cnt.ToString());
                Prompt(string.Format("Directory Count: {0}", dir_cnt));
                for (int i = 0; i < dir_cnt; i++)
                {
                    kvp.SetVal(string.Format("D.{0}", i), GetRelPath(all_dirs[i], root));
                    Prompt(string.Format("Adding Directory: {0}", all_dirs[i]));
                }
                Prompt("--------------------------------");

                kvp.SetVal("F.CNT", file_cnt.ToString());
                Prompt(string.Format("File Count: {0}", file_cnt));
                for (int i = 0; i < file_cnt; i++)
                {
                    kvp.SetVal(string.Format("F.{0}", i), GetRelPath(all_files[i], root));
                    Prompt(string.Format("Adding File: {0}", all_files[i]));
                }
                Prompt("--------------------------------");

                long file_offset_indx = 0;
                for (int i = 0; i < file_cnt; i++)
                {
                    FileInfo fi = new FileInfo(all_files[i]);
                    kvp.SetVal(string.Format("F.{0}.O", i), file_offset_indx.ToString());
                    kvp.SetVal(string.Format("F.{0}.L", i), fi.Length.ToString());
                    file_offset_indx += fi.Length;
                }
                Prompt("--------------------------------");
                return kvp;
            }
        }

        private void Prompt(string InfoStr)
        {
            if (this.OnInfo != null) this.OnInfo(InfoStr + "\r\n");
            Application.DoEvents();
        }


        public bool DoPack(string SourceRootDirPath, string DestFileName, ulong RndSeed, string Psw)
        {
            // File construction:
            // 1- random-seed (64 bit ulong)     bytes 0,1,2,3,4,5,6,7
            // 2- crc (32 bit uint)              bytes 8,9,10,11
            // 3- CIPHEROK (64 bit string)       bytes 12,13,14,15
            // 4- header-length (64 bit ulong)   bytes 16,17,18,19,20,21,22,23
            // 5- header-data (string)           bytes 24,25, ...
            // 6- body            


            if (SourceRootDirPath.Trim() == "") return false;
            uint crc = 0;
            Prompt(string.Format("Paking..."));
            Prompt("--------------------------------");

            List<string> Log = new List<string>();
            BinaryWriter bw = null;
            BinaryReader br = null;
            byte[] buff_read = new byte[1024 * 1024];

            string source_file = null;
            if (File.Exists(SourceRootDirPath))
            {
                source_file = SourceRootDirPath;
            }

            try
            {
                MyCypher mc = new MyCypher(Psw, RndSeed);
                

                KeyValPair kvp = ProvideHeader(SourceRootDirPath);
                byte[] bytes_header = ASCIIEncoding.ASCII.GetBytes(kvp.GetStr());


                bw = new BinaryWriter(new FileStream(DestFileName, FileMode.Create));
                byte[] bytes_rnd_seed = BitConverter.GetBytes(RndSeed);
                
                byte[] bytes_cipher_test = ASCIIEncoding.ASCII.GetBytes("CIPHEROK");
                ulong header_length = (ulong)bytes_header.Length;
                byte[] bytes_header_length = BitConverter.GetBytes(header_length);


                // Random seed is written in plain-text (it is not ciphered)
                bw.Write(bytes_rnd_seed);
                crc ^= GetCrc(ref bytes_rnd_seed);
                //--------------------------------------------------

                
                // CRC is written in plain-text (it is not ciphered)
                bw.Write(crc);
                //--------------------------------------------------

                mc.Cipher(ref bytes_cipher_test);
                bw.Write(bytes_cipher_test);
                crc ^= GetCrc(ref bytes_cipher_test);
                //--------------------------------------------------


                mc.Cipher(ref bytes_header_length);
                bw.Write(bytes_header_length);
                crc ^= GetCrc(ref bytes_header_length);
                //--------------------------------------------------


                mc.Cipher(ref bytes_header);
                bw.Write(bytes_header);
                crc ^= GetCrc(ref bytes_header);
                //--------------------------------------------------

                int file_count = Int16.Parse(kvp.GetVal("F.CNT"));
                string file_full_name = "";

                for (int i = 0; i < file_count; i++)
                {
                    if (source_file != null)
                    {
                        file_full_name = source_file;
                    }
                    else
                    {
                        file_full_name = SourceRootDirPath + "\\" + kvp.GetVal(string.Format("F.{0}", i));
                    }
                    
                    Prompt(string.Format("Packing File: {0}", file_full_name));

                    FileInfo fi = new FileInfo(file_full_name);
                    if (File.Exists(file_full_name) == false)
                    {
                        Log.Add(string.Format("'{0}' not found", file_full_name));
                        continue;
                    }
                    br = new BinaryReader(new FileStream(file_full_name, FileMode.Open));
                    int cnt = 0;
                    while (true)
                    {
                        cnt = br.Read(buff_read, 0, buff_read.Length);
                        if (cnt == 0) break;
                        mc.Cipher(ref buff_read, cnt);
                        bw.Write(buff_read, 0, cnt);
                        crc ^= GetCrc(ref buff_read, cnt);
                    }
                    br.Close();
                }
                bw.Seek(8, SeekOrigin.Begin);
                bw.Write(crc);
                bw.Close();
                Prompt("--------------------------------");
                return true;
            }
            catch(Exception ex)
            {
                if (bw != null)
                {
                    try
                    {
                        bw.Close();
                    }
                    catch { }
                }
                throw new Exception(string.Format("Unable to pack folder '{0}'.\n{1}", ex.Message));
            }
        }

        


        public bool DoUnpack(string DestRootDirPath, string SourceFileName, string Psw)
        {

            // File construction:
            // 1- random-seed (64 bit ulong)     bytes 0,1,2,3,4,5,6,7
            // 2- crc (32 bit uint)              bytes 8,9,10,11
            // 3- CIPHEROK (64 bit string)       bytes 12,13,14,15
            // 4- header-length (64 bit ulong)   bytes 16,17,18,19,20,21,22,23
            // 5- header-data (string)           bytes 24,25, ...
            // 6- body            

            uint crc = 0;
            uint crc_check = 0;
            Prompt(string.Format("Unpacking..."));

            if (Directory.Exists(DestRootDirPath) == true)
            {
                try
                {
                    Directory.Delete(DestRootDirPath, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to remove existing directory\n" + ex.Message);
                    return false;
                }
            }
            else
            {
                Directory.CreateDirectory(DestRootDirPath);
            }

            byte[] buff_read = new byte[1024 * 1024];
            List<string> Log = new List<string>();
            BinaryReader br = null;
            BinaryWriter bw = null;

            try
            {
                // if (File.Exists(SourceFileName) == false) return false;
                br = new BinaryReader(new FileStream(SourceFileName, FileMode.Open));

                // Random seed is written in plain-text (it is not ciphered)
                ulong rnd_seed = br.ReadUInt64();
                crc ^= GetCrc(rnd_seed);
                //--------------------------------------------------


                MyCypher mc = new MyCypher(Psw, rnd_seed);

                crc_check = br.ReadUInt32();

                ulong ciph_test = br.ReadUInt64();
                byte[] bytes_cipher_test = BitConverter.GetBytes(ciph_test);
                crc ^= GetCrc(ref bytes_cipher_test);
                mc.Decipher(ref bytes_cipher_test);
                string ciph_test_str = ASCIIEncoding.ASCII.GetString(bytes_cipher_test);
                if (ciph_test_str != "CIPHEROK")
                {
                    br.Close();
                    Log.Add("Invalid Password");
                    Prompt(string.Format("Invalid Password"));
                    return false;
                }

                ulong header_length = br.ReadUInt64();


                byte[] bytes_header_length = BitConverter.GetBytes(header_length);
                crc ^= GetCrc(ref bytes_header_length);
                mc.Decipher(ref bytes_header_length);
                header_length = BitConverter.ToUInt64(bytes_header_length, 0);
                //--------------------------------------------------


                byte[] bytes_header = new byte[header_length];
                br.Read(bytes_header, 0, bytes_header.Length);
                crc ^= GetCrc(ref bytes_header);
                mc.Decipher(ref bytes_header);
                string header_str = ASCIIEncoding.ASCII.GetString(bytes_header);
                //--------------------------------------------------


                KeyValPair kvp = new KeyValPair(';',':');
                kvp.Fill(header_str);

                int dir_cnt = kvp.GetValAsInt("D.CNT");
                Prompt(string.Format("Directory Count: {0}", dir_cnt));
                string dir = "";
                for (int i = 0; i < dir_cnt; i++)
                {
                    dir = DestRootDirPath + kvp.GetVal(string.Format("D.{0}", i));
                    Directory.CreateDirectory(dir);
                }

                int file_cnt = kvp.GetValAsInt("F.CNT");
                Prompt(string.Format("File Count: {0}", file_cnt));
                string file_full_name = "";
                long file_offset = 0;
                long file_length = 0;
                for (int i = 0; i < file_cnt; i++)
                {
                    string rel_file_name = kvp.GetVal(string.Format("F.{0}", i));
                    file_full_name = DestRootDirPath + rel_file_name;
                    Prompt(string.Format("Unpack File: {0}", file_full_name));
                    bw = new BinaryWriter(new FileStream(file_full_name, FileMode.Create));
                    file_offset = kvp.GetValAsInt64(string.Format("F.{0}.O", i));
                    file_offset += (long)header_length + 8 + 8 + 8 + 4/*CRC*/;
                    file_length = kvp.GetValAsInt64(string.Format("F.{0}.L", i));
                    br.BaseStream.Seek(file_offset, SeekOrigin.Begin);
                    int cnt = 0;
                    long totlal_rem = file_length;
                    int read_cnt = 0;
                    while (true)
                    {
                        read_cnt = buff_read.Length;
                        if (totlal_rem < read_cnt)
                        {
                            read_cnt = (int)totlal_rem;
                        }
                        cnt = br.Read(buff_read, 0, read_cnt);
                        totlal_rem -= cnt;
                        crc ^= GetCrc(ref buff_read, cnt);
                        mc.Decipher(ref buff_read, cnt);

                        bw.Write(buff_read, 0, cnt);
                        if (cnt == 0) break;
                        if (totlal_rem == 0) break;
                    }
                    bw.Close();
                }
                Prompt("--------------------------------");
                
                if (crc_check != crc)
                {
                    Prompt("CRC Missmatch!");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                if (bw != null)
                {
                    try
                    {
                        bw.Close();
                    }
                    catch 
                    { 
                        // 
                    }
                }
                //throw new Exception(string.Format("Unable to pack folder '{0}'.\n{1}", ex.Message));
                return false;
            }

        }

        private uint GetCrc(ulong Data)
        {
            byte[] data = BitConverter.GetBytes(Data);
            return GetCrc(ref data);
        }

        private UInt32 GetCrc(ref byte[] Data)
        {
            return this.GetCrc(ref Data, Data.Length);
        }

        private uint GetCrc(ref byte[] Data, int Count)
        {
            UInt32 crc = 0;
            byte[] crc_bytes = new byte[4];
            int indx = 0;
            
            int i = 0;
            while (indx < Count)
            {
                crc_bytes[i] ^= Data[indx];
                i = (i + 1) % 4;
                indx++;
            }

            crc = BitConverter.ToUInt32(crc_bytes, 0);
            return crc;
        }

        internal bool ShredFiles(string Dir)
        {
            try
            {
                Prompt("Start Shreding...");
                string[] files = this.GetAllFiles(Dir);
                for (int i = 0; i < files.Length; i++)
                {
                    ShredFile(files[i]);
                    RenameToRandom(files[i]);
                }
                //Directory.Delete(Dir, true);
                Prompt("Shred is Done.");
                return true;
            }
            catch(Exception ex)
            {
                string msg = "Shred Failed!\nMessage: " + ex.Message;
                Prompt("Shred Failed! "+ex.Message);
                throw new Exception(msg);
            }
        }

        private void RenameToRandom(string FileName)
        {
            string dir = Path.GetDirectoryName(FileName);
            string rnd_file_name = dir + "\\" + GetRandomFileName();
            File.Move(FileName, rnd_file_name);
        }

        byte[] ShredArray = new byte[1024 * 1024];
        public bool ShredFile(string FileName)
        {
            try
            {
                Prompt("Shred file: " + FileName);
                FileInfo fi = new FileInfo(FileName);
                BinaryWriter bw = new BinaryWriter(fi.OpenWrite());
                long len = fi.Length;
                long cnt = 0;
                while (len > 0)
                {
                    if (len < ShredArray.Length) cnt = ShredArray.Length - len;
                    else cnt = ShredArray.Length;
                    bw.Write(ShredArray, 0, (int)cnt);
                    len -= cnt;
                }
                bw.Close();
                //File.Delete(FileName);
                return true;
            }
            catch (Exception ex)
            {
                string msg = "Shred Failed!\nMessage: " + ex.Message;
                Prompt("Shred Failed! " + ex.Message);
                throw new Exception(msg);
            }
        }

        string rnd_str = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";
        int seed = 100;
        public string GetRandomStr(int Count)
        {
            Random rnd = new Random(seed++);
            string str = "";
            for (int i = 0; i < Count; i++)
            {
                str += rnd_str[rnd.Next(0, rnd_str.Length)].ToString();
            }
            return str;
        }

        public string GetRandomFileName()
        {
            string fn = this.GetRandomStr(10) + "." + this.GetRandomStr(3);
            return fn;
        }


    }

}