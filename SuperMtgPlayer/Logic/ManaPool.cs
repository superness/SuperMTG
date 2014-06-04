using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Logic
{
    public class ManaPool : Singleton<ManaPool>
    {
        public enum ManaType
        {
            White,
            Blue,
            Red,
            Black,
            Green,
            Colorless
        }

        public Dictionary<ManaType, int> ManaInPool = new Dictionary<ManaType, int>();

        public ManaPool()
        {
            ManaInPool[ManaType.White] = 0;
            ManaInPool[ManaType.Blue] = 0;
            ManaInPool[ManaType.Red] = 0;
            ManaInPool[ManaType.Black] = 0;
            ManaInPool[ManaType.Green] = 0;
            ManaInPool[ManaType.Colorless] = 0;
        }

        public void AddToPool(string manaString)
        {
            Dictionary<ManaType, int> convertedMana = this.ManaStringToDictionary(manaString);

            foreach(KeyValuePair<ManaType, int> pair in convertedMana)
            {
                this.AddToPool(pair.Key);
            }
        }

        public bool HaveManaForSpell(string manaString, int xValue = 0)
        {
            Dictionary<ManaType, int> convertedMana = this.ManaStringToDictionary(manaString);

            foreach(KeyValuePair<ManaType, int> pair in convertedMana)
            {
                if(this.ManaInPool[pair.Key] < pair.Value)
                {
                    return false;
                }
            }

            if (convertedMana.ContainsKey(ManaType.Colorless))
                return this.ManaInPool[ManaType.Colorless] >= (convertedMana[ManaType.Colorless] + xValue);
            else
                return this.ManaInPool[ManaType.Colorless] >= xValue;
        }

        public void RemoveManaFromPool(string manaString, int xValue = 0)
        {
            Dictionary<ManaType, int> convertedMana = this.ManaStringToDictionary(manaString);

            foreach(KeyValuePair<ManaType, int> pair in convertedMana)
            {
                this.ManaInPool[pair.Key] -= pair.Value;
            }

            this.ManaInPool[ManaType.Colorless] -= xValue;
        }

        private Dictionary<ManaType, int> ManaStringToDictionary(string manaString)
        {
            manaString = manaString.Replace("}{", ",");
            manaString = manaString.Trim(new char[] { '{', '}' });
            string[] manaValues = manaString.Split(new string[] {","}, StringSplitOptions.None);

            List<ManaType> manaTypes = new List<ManaType>();

            foreach(string s in manaValues)
            {
                if (s == "W")
                {
                    manaTypes.Add(ManaType.White);
                }
                else if (s == "U")
                {
                    manaTypes.Add(ManaType.Blue);
                }
                else if (s == "R")
                {
                    manaTypes.Add(ManaType.Red);
                }
                else if (s == "G")
                {
                    manaTypes.Add(ManaType.Green);
                }
                else if (s == "B")
                {
                    manaTypes.Add(ManaType.Black);
                }
                else if(s != "X")
                {
                    // Parse {1}..{3} etc
                    string numStr = s.Trim(new char[] { '{', '}' });
                    int num = int.Parse(numStr);

                    for(int i = 0; i < num; ++i)
                    {
                        manaTypes.Add(ManaType.Colorless);
                    }
                }
            }

            Dictionary<ManaType, int> manaDictionary = new Dictionary<ManaType, int>();

            foreach(ManaType type in manaTypes)
            {
                if (manaDictionary.ContainsKey(type))
                    manaDictionary[type] += 1;
                else
                    manaDictionary[type] = 1;
            }

            return manaDictionary;
        }

        private void AddToPool(ManaType mana)
        {
            this.ManaInPool[mana] += 1;
        }
    }
}
