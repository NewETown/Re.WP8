using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re.Common
{
    /// <summary>
    /// Keyword object
    /// </summary>
    class Keyword : IEquatable<Keyword>
    {
        // Constructors
        public Keyword()
        {
            this.Count = 1;
        }
        public Keyword(int count)
        {
            this.Count = count;
        }
        public Keyword(string word)
        {
            this.Word = word;
            this.Count = 1;
        }
        public Keyword(string word, int count)
        {
            this.Word = word;
            this.Count = count;
        }

        // Properties
        public string Word { get; set; }
        public int Count { get; private set; }

        // Methods
        public void AddCount()
        {
            this.Count += 1;
        }

        public bool Equals(Keyword other)
        {
            if (this.Word == other.Word)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
