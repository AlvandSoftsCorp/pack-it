using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackIt
{
    public class KeyValPair
    {
        private List<string> Keys = new List<string>();
        private List<string> Vals = new List<string>();

        private char ItemSeparator = '\0';
        private char ElementSeparator = '\0';

        public KeyValPair(char ItemSeparator, char ElementSeparator)
        {
            this.ItemSeparator = ItemSeparator;
            this.ElementSeparator = ElementSeparator;
        }


        public string GetVal(string Key)
        {
            int indx = this.GetIndexOf(Key);
            if (indx == -1) return null;
            return Vals[indx];
        }

        private int GetIndexOf(string Key)
        {
            for (int i = 0; i < Keys.Count; i++)
            {
                if (Key == Keys[i]) return i;
            }
            return -1;
        }

        public void Add(string Key, string Val)
        {
            string k, v;

            k = Key.Trim();
            v = Val.Trim();

            //k = FeedWildChar(k, this.ItemSeparator);
            //k = FeedWildChar(k, this.ElementSeparator);

            //v = FeedWildChar(v, this.ItemSeparator);
            //v = FeedWildChar(v, this.ElementSeparator);

            Keys.Add(k);
            Vals.Add(v);
        }

        private string FeedWildChar(string Str, char ch)
        {
            string s = ch.ToString();
            string ss = s + s;
            string str = Str.Replace(s, ss);
            return str;
        }



        public void SetVal(string Key, string Val)
        {
            string k, v;

            k = Key.Trim();
            v = Val.Trim();

            //k = FeedWildChar(k, this.ItemSeparator);
            //k = FeedWildChar(k, this.ElementSeparator);

            //v = FeedWildChar(v, this.ItemSeparator);
            //v = FeedWildChar(v, this.ElementSeparator);

            int indx = this.GetIndexOf(k);
            if (indx == -1)
            {
                Add(k, v);
            }
            else
            {
                Keys[indx] = k;
                Vals[indx] = v;
            }
        }

        //public void Fill(string KeyValStr)
        //{
        //    if (KeyValStr == null) return;
        //    if (KeyValStr == "") return;

        //    char[] sep_item = { ';', '\n' };
        //    char[] sep_key_val = { ':', '=' };

        //    string[] items = KeyValStr.Split(sep_item, StringSplitOptions.RemoveEmptyEntries);

        //    foreach (string item in items)
        //    {
        //        string[] key_vals = item.Split(sep_key_val, StringSplitOptions.RemoveEmptyEntries);
        //        {
        //            if (key_vals.Length != 2) continue;
        //            string key = key_vals[0].Trim();
        //            string val = key_vals[1].Trim();
        //            if (key == "") continue;
        //            if (val == "") continue;

        //            this.SetVal(key, val);
        //        }
        //    }
        //}
        
        
        public void Fill(string KeyValStr)
        {
            if (KeyValStr == null) return;
            if (KeyValStr == "") return;

            char[] sep_item = { ItemSeparator };
            char[] sep_key_val = { ElementSeparator };

            //string[] items = KeyValStr.Split(sep_item, StringSplitOptions.RemoveEmptyEntries);
            string[] items = Split(KeyValStr, this.ItemSeparator);

            foreach (string item in items)
            {
                //string[] key_vals = item.Split(sep_key_val, StringSplitOptions.RemoveEmptyEntries);
                string[] key_vals = Split(item, this.ElementSeparator);
                {
                    if (key_vals.Length != 2) continue;
                    string key = key_vals[0].Trim();
                    string val = key_vals[1].Trim();
                    if (key == "") continue;
                    if (val == "") continue;

                    this.SetVal(key, val);
                }
            }
        }


        public string GetStr()
        {
            string k, v;
            string res = "";

            for (int i = 0; i < Keys.Count; i++)
            {
                k = Keys[i];
                v = Vals[i];

                k = FeedWildChar(k, this.ItemSeparator);
                k = FeedWildChar(k, this.ElementSeparator);

                v = FeedWildChar(v, this.ItemSeparator);
                v = FeedWildChar(v, this.ElementSeparator);

                res += string.Format("{0}{1}{2}{3} ", k, this.ElementSeparator, v, ItemSeparator);
            }
            return res;
        }

        public void Clear()
        {
            this.Keys.Clear();
            this.Vals.Clear();
        }

        public bool Remove(string Key)
        {
            int indx = this.GetIndexOf(Key);
            if (indx == -1) return false;
            Keys.RemoveAt(indx);
            Vals.RemoveAt(indx);
            return true;
        }


        internal int GetValAsInt(string Key)
        {
            int val_int = 0;
            try
            {
                val_int = int.Parse(this.GetVal(Key));
                return val_int;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Value '{0}' is not a valid integer", val_int));
            }
        }
        
        
        internal long GetValAsInt64(string Key)
        {
            Int64 val_int = 0;
            try
            {
                val_int = Int64.Parse(this.GetVal(Key));
                return val_int;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Value '{0}' is not a valid integer", val_int));
            }
        }

        private static string[] Split(string InputStr, char Separator)
        {
            List<string> p = new List<string>();
            if (InputStr == null) return p.ToArray();
            string input_str = InputStr.Trim();
            if (input_str == null) return p.ToArray();
            //string tag = "#A1Q8B9E0R4#";
            
            string t = "";
            string tag = string.Format("#{0}#",t);
            Random rnd = new Random();
            string[]d={"0","1","2","3","4","5","6","7","8","9"};
            while (input_str.IndexOf(tag) != -1)
            {
                t += d[rnd.Next(0, 10)];
                tag = string.Format("#{0}#", t);
            }

            string str = input_str.Replace(Separator.ToString() + Separator.ToString(), tag);
            string[] parts = str.Split(new char[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Replace(tag, Separator.ToString());
                if (parts[i].Trim() != "") p.Add(parts[i]);
            }
            return p.ToArray();
        }

        public int Count
        {
            get
            {
                return this.Keys.Count;
            }
        }

        public string GetKey(int Index)
        {
            try
            {
                return Keys[Index];
            }
            catch
            {
                throw new Exception("Invalid Index.");
            }
        }
        
        
        public string GetVal(int Index)
        {
            try
            {
                return Vals[Index];
            }
            catch
            {
                throw new Exception("Invalid Index.");
            }
        }

    }
}
