using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMtgPlayer.Logic
{
    public class ManaPool
    {
        public enum ManaType
        {
            White,
            Blue,
            Red,
            Black,
            Green,
            Colorless,

            Count
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

        public void EmptyPool()
        {
            for(ManaType type = ManaType.White; type < ManaType.Count; ++type)
            {
                ManaInPool[type] = 0;
            }
        }

        public void AddToPool(string manaString)
        {
            Dictionary<ManaType, int> convertedMana = this.ManaStringToDictionary(manaString);

            foreach(KeyValuePair<ManaType, int> pair in convertedMana)
            {
                this.AddToPool(pair.Key);
            }
        }

        private int TotalManaCount(Dictionary<ManaType, int> dictionary)
        {
            int numMana = 0;
            foreach (KeyValuePair<ManaType, int> kvp in dictionary)
            {
                numMana += kvp.Value;
            }
            return numMana;
        }

        public bool HaveManaForSpell(string manaString, int xValue = 0)
        {
            Dictionary<ManaType, int> convertedMana = this.ManaStringToDictionary(manaString);

            foreach(KeyValuePair<ManaType, int> pair in convertedMana)
            {
                if (pair.Key == ManaType.Colorless)
                    continue;

                // Make sure we have colored mana
                if(this.ManaInPool[pair.Key] < pair.Value)
                {
                    return false;
                }
            }

            // Check that we have mana to cover colorless cost
            return this.TotalManaCount(this.ManaInPool) >= (this.TotalManaCount(convertedMana) + xValue);
        }

        public void RemoveManaFromPool(string manaString, int xValue = 0)
        {
            Dictionary<ManaType, int> convertedMana = this.ManaStringToDictionary(manaString);

            foreach (KeyValuePair<ManaType, int> pair in convertedMana)
            {
                if (pair.Key == ManaType.Colorless)
                    continue;

                this.ManaInPool[pair.Key] -= pair.Value;
            }

            // Pay colorless mana
            int totalColorless = convertedMana.ContainsKey(ManaType.Colorless) ? convertedMana[ManaType.Colorless] + xValue : xValue;

            // Take it from colorless first
            {
                int manaTake = Math.Min(totalColorless, this.ManaInPool[ManaType.Colorless]);
                this.ManaInPool[ManaType.Colorless] -= manaTake;
                totalColorless -= manaTake;
            }

            // Then from any other mana
            Dictionary<ManaType, int> toTake = new Dictionary<ManaType, int>();
            foreach (KeyValuePair<ManaType, int> pair in this.ManaInPool)
            {
                if (totalColorless > 0)
                {
                    int manaTake = Math.Min(totalColorless, this.ManaInPool[pair.Key]);
                    toTake[pair.Key] = manaTake;
                    totalColorless -= manaTake;
                }
            }

            foreach(KeyValuePair<ManaType, int> pair in toTake)
            {
                this.ManaInPool[pair.Key] -= pair.Value;
            }
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
