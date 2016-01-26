using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolution.game;

//Difficulty to get past 300 fitness, converging too fast...
//Ideas: More random AI at each generation -- More mutation -- complexify the way the Seed renders's the start board

namespace Evolution.AI
{
    class EvolutionAI
    {
        private Game game;

        private const int START_ZONE_WIDTH = 20;
        private const int START_ZONE_HEIGHT = 20;
        private const int POPULATION_SIZE = 44;
        private Random RNG = new Random();

        private List<KeyValuePair<string, int>> population;
        private List<KeyValuePair<string, int>> populationBuffer;
        private List<KeyValuePair<string, int>> newPopulation;

        public EvolutionAI(Game _game)
        {
            newPopulation = new List<KeyValuePair<string, int>>();
            population = new List<KeyValuePair<string, int>>();
            populationBuffer = new List<KeyValuePair<string, int>>();
            game = _game;
        }


        public void startEvolution(int _nbOfGeneration)
        {
            GenerateRandomPopulation();            

            for (int i = 0; i < _nbOfGeneration; i++)
            {
                Console.WriteLine("========== GENERATION " + i + " ==========");
                populationBuffer = new List<KeyValuePair<string, int>>(population);
                Console.WriteLine("NB of individuals: " + populationBuffer.Count);
                foreach (var pair in populationBuffer)
                {
                    game.ReinitBoard();
                    setValueFromKey(pair.Key, game.StartGame(pair.Key), population);
                }

                SelectPopulation();
                CrossPopulation();
                applyMutations();
                newPopulation = new List<KeyValuePair<string, int>>();
            }
        }

        private void SelectPopulation()
        {
            int i = 1;
            foreach (var pair in population.OrderBy(j => j.Value))
            {
                if (i <= POPULATION_SIZE/2)
                {
                    removeFromKey(pair.Key, population);
                }
                else if (i > POPULATION_SIZE - 2)
                {
                    newPopulation.Add(new KeyValuePair<string, int>(pair.Key,pair.Value));
                    removeFromKey(pair.Key, population);
                }
                i++;
            }
        }

        private void CrossPopulation()
        {
            string parent1 = "";
            foreach (var pair in population.OrderBy(j => j.Value))
            {
                if (parent1 == "")
                {
                    parent1 = pair.Key;
                    continue;
                }
                
                int crossOverPoint = RNG.Next(1,400);
                newPopulation.Add(new KeyValuePair<string, int>(parent1.Substring(0, crossOverPoint) + pair.Key.Substring(crossOverPoint, 400 - crossOverPoint), 0));
                newPopulation.Add(new KeyValuePair<string, int>(pair.Key.Substring(0, crossOverPoint) + parent1.Substring(crossOverPoint, 400 - crossOverPoint), 0));

                crossOverPoint = RNG.Next(1, 400);
                newPopulation.Add(new KeyValuePair<string, int>(parent1.Substring(0, crossOverPoint) + pair.Key.Substring(crossOverPoint, 400 - crossOverPoint), 0));
                newPopulation.Add(new KeyValuePair<string, int>(pair.Key.Substring(0, crossOverPoint) + parent1.Substring(crossOverPoint, 400 - crossOverPoint), 0));

                //to reintroduce parents in next generation
                //newPopulation.Add(new KeyValuePair<string, int>(pair.Key, 0));
                //newPopulation.Add(new KeyValuePair<string, int>(parent1, 0));

                parent1 = "";
            }
            population = new List<KeyValuePair<string, int>>(newPopulation);
            population.Add(new KeyValuePair<string, int>(generateRandomSeed(), 0));
            population.Add(new KeyValuePair<string, int>(generateRandomSeed(), 0));
            populationBuffer = new List<KeyValuePair<string, int>>(population);
        }

        private void applyMutations()
        {
            population = new List<KeyValuePair<string, int>>();

            int i = 0;
            foreach (var pair in populationBuffer.OrderBy(j => j.Value))
            {
                if (i == 1)
                {
                    population.Add(new KeyValuePair<string, int>(populationBuffer[i].Key, 0));
                    i++;
                    continue;
                }

                population.Add(new KeyValuePair<string, int>(mutateSeed(populationBuffer[i].Key), 0));

                i++;
            }
        }

        private string mutateSeed(string _originalSeed)
        {
            int numberOfMutation = RNG.Next(1, 10);
            int location = 0;
            string mutatedString = _originalSeed;

            for (int i = 0; i < numberOfMutation; i++)
            {
                location = RNG.Next(0, 400);
                if (_originalSeed[location] == '1')
                {
                    mutatedString = mutatedString.Remove(location, 1);
                    mutatedString = mutatedString.Insert(location, "0");
                }
                else
                {
                    mutatedString.Remove(location, 1);
                    mutatedString.Insert(location, "1");
                }
            }

            return mutatedString;
        }

        private void GenerateRandomPopulation()
        {
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                population.Add(new KeyValuePair<string, int>(generateRandomSeed(), 0));
            }                
        }

        private string generateRandomSeed()
        {
            string stringBuilder = "";
            for (int x = 0; x < START_ZONE_WIDTH; x++)
            {
                for (int y = 0; y < START_ZONE_HEIGHT; y++)
                {
                    stringBuilder += RNG.Next(0, 2);
                }
            }
            return stringBuilder;
        }


        private int getValueFromKey(string key, List<KeyValuePair<string, int>> list){
            for (int i = 0; i < list.Count; i++)
            {
                if (key == list[i].Key)
                {
                    return list[i].Value;
                }
            }
            return 0;
        }

        private void removeFromKey(string key, List<KeyValuePair<string, int>> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (key == list[i].Key)
                {
                    list.RemoveAt(i);
                    return;
                }
            }
        }

        private void setValueFromKey(string key, int value, List<KeyValuePair<string, int>> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (key == list[i].Key)
                {
                    list.Add(new KeyValuePair<string,int>(key,value));
                    list.RemoveAt(i);
                }
            }
        }
    }
}
