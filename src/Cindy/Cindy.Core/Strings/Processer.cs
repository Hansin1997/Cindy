using System.Collections.Generic;
using System.Text;

namespace Cindy.Strings
{
    public class Processer
    {
        public delegate string Handler(string key);

        protected Dictionary<string, Handler> map;

        public Processer()
        {
            map = new Dictionary<string, Handler>();
        }

        public Processer AddHandler(Handler handler, char startChar = '{', char endChar = '}')
        {
            if (startChar == endChar)
                throw new System.Exception("start char can't equal to end char!");
            if (startChar == 0 || endChar == 0)
                throw new System.Exception("start char or end char can't be empty!");
            if (handler != null)
                map[("" + startChar + endChar)] = handler;
            return this;
        }

        public string GetString(string input, int start, int length)
        {

            if (map == null || input == null || input.Trim().Length == 0)
                return input;
            StringBuilder builder = new StringBuilder(input.Length);
            Handler[] handlerMap = new Handler[256];
            char[] endCharMap = new char[256];
            foreach (KeyValuePair<string, Handler> kv in map)
            {
                char startChar = kv.Key[0], endChar = kv.Key[1];

                handlerMap[startChar] = kv.Value;
                endCharMap[startChar] = endChar;
            }

            int index = -1, count = 0;
            for (int i = start, right = start + length; i <= right; i++)
            {

                if (i == right)
                {
                    if (index != -1)
                        builder.Append(input, index, right - index);
                    break;
                }
                char c = input[i];

                if (index == -1)
                {
                    if (c <= 255 && handlerMap[c] != null)
                        index = i;
                    else
                        builder.Append(c);
                }
                else
                {
                    if (c == input[index])
                        count++;
                    else if (c == endCharMap[input[index]])
                    {

                        if (count == 0)
                        {
                            string tmp = handlerMap[input[index]](GetString(input, index + 1, i - index - 1));

                            builder.Append(tmp);
                            index = -1;
                        }
                        else
                            count--;
                    }
                }
            }
            return builder.ToString();
        }

        public string GetString(string input)
        {
            return GetString(input, 0, input.Length);
        }
    }
}
