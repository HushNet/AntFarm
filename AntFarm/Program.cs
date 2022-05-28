using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AntFarm
{
    enum EResources
    {
        Stones,
        Sticks,
        Leaves,
        Dewdrops
    }

    enum Specials
    {
        Mythic,
        Stupid,
        Fat,
        Lazy,
        Peaceful,
        Suspectful,
        Veteran,
        Phoenix,
        Epic,
        QueenFavorite,
        GodMode
    }

    class Process
    {
        public static int DryTimer = 14;
        public static int Day = 0;

        public static Random rand = new Random();

        public static Colony greenColony = new Colony("green", new Queen("Blanka", 16, 17, 8, 3, 3,1,3),
            new Dictionary<string, int>() { { "workers", 13 }, { "warriors", 8 }, { "unique", 1 } });

        public static Colony orangeColony = new Colony("orange", new Queen("Margrett", 20, 7, 15, 3, 5,3,3),
            new Dictionary<string, int>() { { "workers", 14 }, { "warriors", 6 }, { "unique", 1 } });

        public static Colony blackColony = new Colony("black", new Queen("Sofie", 17, 7, 16, 1, 5,2,3),
            new Dictionary<string, int>() { { "workers", 16 }, { "warriors", 5 }, { "unique", 1 } });

        public static List<Colony> colonies = new List<Colony>() { greenColony, orangeColony, blackColony };

        public static Heap heap1 = new Heap(new Resources(0, 0, 30, 0));
        public static Heap heap2 = new Heap(new Resources(0, 24, 33, 36));
        public static Heap heap3 = new Heap(new Resources(27, 0, 32, 46));
        public static Heap heap4 = new Heap(new Resources(0, 0, 45, 0));
        public static Heap heap5 = new Heap(new Resources(0, 39, 14, 0));

        public static List<Heap> heaps = new List<Heap>() { heap1, heap2, heap3, heap4, heap5 };
        

        public static AntWorker eliteWorker = new AntWorker(8, 4, greenColony.colonyName, new List<EResources>()
                { EResources.Dewdrops, EResources.Dewdrops }, 2,
            new Resources(0, 0, 0, 0), new List<Specials>() { }, "элитный");

        public static AntWorker oldStupidWorker = new AntWorker(2, 4, greenColony.colonyName, new List<EResources>()
                { EResources.Stones, EResources.Dewdrops }, 1,
            new Resources(0, 0, 0, 0), new List<Specials>() { Specials.Stupid },"старший глупый");

        public static AntWorker advancedWorker = new AntWorker(6, 2, blackColony.colonyName, new List<EResources>()
                { EResources.Sticks, EResources.Leaves }, 2,
            new Resources(0, 0, 0, 0), new List<Specials>(),"продвинутый");

        public static AntWorker eliteQueenFavoriteWorker = new AntWorker(8, 4, blackColony.colonyName,
            new List<EResources>()
                { EResources.Sticks, EResources.Leaves }, 2,
            new Resources(0, 0, 0, 0), new List<Specials>() { Specials.QueenFavorite },"элитный любимчик королевы");

        public static AntWorker ledgendaryWorker = new AntWorker(10, 6, orangeColony.colonyName, new List<EResources>()
                { EResources.Stones, EResources.Dewdrops, EResources.Sticks }, 3,
            new Resources(0, 0, 0, 0), new List<Specials>() { },"легендарный");

        public static AntWorker oldVeteranWorker = new AntWorker(2, 1, orangeColony.colonyName, new List<EResources>()
                { EResources.Stones, EResources.Sticks }, 1,
            new Resources(0, 0, 0, 0), new List<Specials>() { Specials.Veteran },"старший ветеран");

        public static AntWarrior legendaryWarrior = new AntWarrior(10, 6, 6, 1, 3, greenColony.colonyName,
            new List<Specials>() { },"легендарный");

        public static AntWarrior fatAdvancedWarrior = new AntWarrior(6, 2, 4, 1, 2, greenColony.colonyName,
            new List<Specials>() { Specials.Fat },"продвинутый толстый");

        public static AntWarrior advancedWarrior = new AntWarrior(6, 2, 4, 1, 2, orangeColony.colonyName,
            new List<Specials>() { },"продвинутый");

        public static AntWarrior elitePhoenixWarrior = new AntWarrior(8, 4, 4, 2, 2, orangeColony.colonyName,
            new List<Specials>() { Specials.Phoenix },"элитный феникс");

        public static AntWarrior eliteWarrior = new AntWarrior(8, 4, 4, 2, 2, blackColony.colonyName,
            new List<Specials>() { },"элитный");

        public static AntWarrior legendaryMythicWarrior = new AntWarrior(10, 6, 6, 1, 3, blackColony.colonyName,
            new List<Specials>() { Specials.Mythic },"легендарный мифический");

        public static UniqueAnt Medvedka = new UniqueAnt(24, 8, greenColony.colonyName,
            new List<Specials>() { Specials.Lazy, Specials.Peaceful, Specials.Suspectful },"обычный - Медведка");

        public static UniqueAnt Skarabei = new UniqueAnt(16, 8, orangeColony.colonyName,
            new List<Specials>() { Specials.Lazy, Specials.Peaceful, Specials.Epic },"обычный - Скарабей");

        public static UniqueAnt Termit = new UniqueAnt(25, 7, blackColony.colonyName,
            new List<Specials>() { Specials.Lazy, Specials.Peaceful, Specials.GodMode, Specials.Epic },"обычный - Термит");

        public static void StartDay()
        {
            
            Day++;
            Console.WriteLine("//////////////////////////////");
            Console.WriteLine("Экран 1 начало хода");
            Console.WriteLine("------------------------------");
            ShowStartDayInfo();

            int colonyRandomID = rand.Next(0, colonies.Count);

            for (int i = colonyRandomID; i < colonies.Count; i++)
            {
                TripAllAnts(colonies[i]);
            }
            for (int i = colonyRandomID-1; i >= 0; i--)
            {
                TripAllAnts(colonies[i]);
            }
            

            SendTripMessage();

            FightAllHeaps();
            
            for (int i = 0; i < heaps.Count; i++)
            {
                for (int j = 0; j < heaps[i].visitors.Count; j++)
                {
                    heaps[i].visitors[j].BackToColony();
                }

                heaps[i].visitors.Clear();
            }

            SendBackMessage();
            


            for (int i = 0; i < heaps.Count; i++)
            {
                heaps[i].allowModificators = true;
            }

            for (int i = 0; i < colonies.Count; i++)
            {
                colonies[i].thisDayResources.ClearValues();
                colonies[i].diesAnts.ClearAll();
                colonies[i].newAnts.ClearAll();
            }
        }

        public static void FightAllHeaps()
        {
            int count = 0;
            for (int i = 0; i < heaps.Count; i++)
            {
                heaps[i].Fight();
                count++;
                for (int j = 0; j < heaps[i].visitors.Count; j++)
                {
                    Ant currentAnt = heaps[i].visitors[j];
                    if (currentAnt.health <= 0)
                    {
                        if (currentAnt.specials.Contains(Specials.Phoenix))
                        {
                            heaps[i].visitors.Remove(currentAnt);
                            currentAnt.health = currentAnt.startHealth / 2;
                        }
                        else
                        {
                            heaps[i].visitors.Remove(currentAnt);
                            currentAnt.mainColony.population.Remove(currentAnt);
                            currentAnt.mainColony.diesAnts.Add(currentAnt,currentAnt.mainColony);
                        }
                    }
                }
            }
        }

        public static void TripAllAnts(Colony colony)
        {
            for (int i = 0; i < colony.population.antWarriorsPopulation.Count; i++)
            {
                colony.population.antWarriorsPopulation[i].Trip();
            }
            for (int i = 0; i < colony.population.antWorkersPopulation.Count; i++)
            {
                colony.population.antWorkersPopulation[i].Trip();
            }

            if (colony.population.uniqueAnt != null)
            {
                colony.population.uniqueAnt.Trip();
            }
            if (colony.queen.currentLarvas.Count <= 0 && colony.queen.queenLarva == null)
                colony.queen.CreateLarvas(20);
        }

        public static void SendTripMessage()
        {
            Console.WriteLine("Экран 2 - Поход");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Начало дня");
            for (int i = 0; i < colonies.Count; i++)
            {
                int workers = 0, warriors = 0, unique = 0;
                for (int j = 0; j < heaps.Count; j++)
                {
                    for (int k = 0; k < heaps[j].visitors.Count; k++)
                    {
                        if (heaps[j].visitors[k].mainColony.colonyNumber == colonies[i].colonyNumber)
                        {
                            if (heaps[j].visitors[k].GetType() == typeof(AntWorker))
                            {
                                workers++;
                            }else if (heaps[j].visitors[k].GetType() == typeof(AntWarrior))
                            {
                                warriors++;
                            }
                            else if (heaps[j].visitors[k].GetType() == typeof(UniqueAnt))
                            {
                                unique++;
                            }
                        }
                    }

                    Console.WriteLine($"С колонии {colonies[i].colonyName} {colonies[i].colonyNumber} отправились: р={workers}, в={warriors}, о={unique} на кучу {j+1}");
                }
            }
        }
        
        public static void SendBackMessage()
        {
            Console.WriteLine("Конец дня");
            for (int i = 0; i < colonies.Count; i++)
            {
                

                Console.WriteLine($"В колонию {colonies[i].colonyName} {colonies[i].colonyNumber} вернулись: " +
                                      $"р={colonies[i].population.antWorkersPopulation.Count}, в={colonies[i].population.antWarriorsPopulation.Count}, о={(colonies[i].population.uniqueAnt!=null?1:0)}");
                Console.Write($"Добыто ресурсов: ");
                colonies[i].thisDayResources.ShowValues();
                Console.WriteLine($"Потери: р={colonies[i].diesAnts.antWorkersPopulation.Count}, в={colonies[i].diesAnts.antWarriorsPopulation.Count}, о={(colonies[i].diesAnts.uniqueAnt!=null?1:0)}");
                for (int j = 0; j < colonies.Count; j++)
                {
                    colonies[j].TryBornLarvas();
                }
                Console.WriteLine($"Выросли: р={colonies[i].newAnts.antWorkersPopulation.Count}, в={colonies[i].newAnts.antWarriorsPopulation.Count}, о={(colonies[i].newAnts.uniqueAnt!=null?1:0)}");
                Console.WriteLine($"Личинки растут: {colonies[i].queen.currentLarvas.Count}");
                
            }
        }

        public static void ShowStartDayInfo()
        {
            Console.WriteLine($"День {Day} (До засухи осталось {DryTimer - Day} дней)");
            for (int i = 0; i < colonies.Count; i++)
            {
                Console.WriteLine($"Колония '{colonies[i].colonyName}' {colonies[i].colonyNumber} ");
                Console.WriteLine($"---Королева {colonies[i].queen.name}, Личинок: {colonies[i].queen.currentLarvas.Count}");
                Console.WriteLine($"---Ресурсы: к={colonies[i].resources.stones}, л={colonies[i].resources.leaves}," +
                                  $" в={colonies[i].resources.sticks}, р={colonies[i].resources.dewdrops}");
                Console.WriteLine(
                    $"---Популяция: р={colonies[i].population.antWorkersPopulation.Count}, в={colonies[i].population.antWarriorsPopulation.Count}," +
                    $" о={(colonies[i].population.uniqueAnt != null ? 1 : 0)}");
            }

            Console.WriteLine($"Куча 1: к={heap1.resources.stones}, л={heap1.resources.leaves}," +
                              $" в={heap1.resources.sticks}, р={heap1.resources.dewdrops}");
            Console.WriteLine($"Куча 2: к={heap2.resources.stones}, л={heap2.resources.leaves}," +
                              $" в={heap2.resources.sticks}, р={heap2.resources.dewdrops}");
            Console.WriteLine($"Куча 3: к={heap3.resources.stones}, л={heap3.resources.leaves}," +
                              $" в={heap3.resources.sticks}, р={heap3.resources.dewdrops}");
            Console.WriteLine($"Куча 4: к={heap4.resources.stones}, л={heap4.resources.leaves}," +
                              $" в={heap4.resources.sticks}, р={heap4.resources.dewdrops}");

            Console.WriteLine($"Глобальный эффект X: ");
            Console.WriteLine();
        }
        

        static void Main(string[] args)
        {
            greenColony.queen.aviableWorkersToRecruit = new List<AntWorker>() { eliteWorker, oldStupidWorker };
            greenColony.queen.aviableWarriorsToRecruit = new List<AntWarrior>() { legendaryWarrior, fatAdvancedWarrior };
            greenColony.queen.aviableUniqueAntToRecruit = (UniqueAnt)Medvedka.Clone(greenColony);

            orangeColony.queen.aviableWorkersToRecruit = new List<AntWorker>() { ledgendaryWorker, oldVeteranWorker };
            orangeColony.queen.aviableWarriorsToRecruit = new List<AntWarrior>() { advancedWarrior, elitePhoenixWarrior };
            orangeColony.queen.aviableUniqueAntToRecruit = (UniqueAnt)Skarabei.Clone(orangeColony);

            blackColony.queen.aviableWorkersToRecruit = new List<AntWorker>() { advancedWorker, eliteQueenFavoriteWorker };
            blackColony.queen.aviableWarriorsToRecruit = new List<AntWarrior>() { eliteWarrior, legendaryMythicWarrior };
            blackColony.queen.aviableUniqueAntToRecruit = (UniqueAnt)Termit.Clone(blackColony);

            for (int i = 0; i < colonies.Count; i++)
            {
                InitializeAnts(colonies[i]);
            }
            
            StartDay();

            while (true)
            {
                Console.WriteLine("Введите номер команды:");
                Console.WriteLine($"1-Следующий день({Day+1}), 2-Информация о колонии, 3-Информация о муравье");
                int ch = Int32.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1:
                        StartDay();
                        break;
                    case 2:
                        Console.WriteLine($"Введите номер колонии: (1-{colonies.Count})");
                        int colonyNumber = Int32.Parse(Console.ReadLine())-1;
                        colonies[colonyNumber].ShowInfo();
                        break;
                    case 3:
                        Console.WriteLine($"Введите номер колонии: (1-{colonies.Count})");
                        int antColonyNumber = Int32.Parse(Console.ReadLine())-1;
                        Console.WriteLine($"Укажите вид муравья(1-Рабочий, 2-Воин, 3-Особый)");
                        int antType = Int32.Parse(Console.ReadLine());
                        switch (antType)
                        {
                            case 1:
                                Console.WriteLine($"Введите номер муравья(1-{colonies[antColonyNumber].population.antWorkersPopulation.Count}):");
                                int antWorkerID = Int32.Parse(Console.ReadLine())-1;
                                colonies[antColonyNumber].population.antWorkersPopulation[antWorkerID].Speak();
                                break;
                            case 2:
                                Console.WriteLine($"Введите номер муравья(1-{colonies[antColonyNumber].population.antWarriorsPopulation.Count}):");
                                int antWarriorID = Int32.Parse(Console.ReadLine())-1;
                                colonies[antColonyNumber].population.antWarriorsPopulation[antWarriorID].Speak();
                                break;
                            case 3:
                                if (colonies[antColonyNumber].population.uniqueAnt != null)
	                            {
                                    colonies[antColonyNumber].population.uniqueAnt.Speak();
                                }
                                else
	                            {
                                         Console.WriteLine("Особое насекомое отсутствует");
	                            }
                                break;
                        }
                        break;
                }

                if (Day == DryTimer)
                {
                    break;
                }
                Console.WriteLine();
            }
            Colony maxColony = colonies[0];
            for (int i = 0; i < colonies.Count; i++)
            {
                
                if (colonies[i].resources.SummAllResources() > maxColony.resources.SummAllResources())
                {
                    maxColony = colonies[i];
                }
            }

            Console.WriteLine($"Колония {maxColony.colonyName} {maxColony.colonyNumber} Победила!");
            
        }

        public static void InitializeAnts(Colony colony)
        {
            for (int i = 0; i < colony.startPopulation["workers"]; i++)
            {
                int id = rand.Next(0, colony.queen.aviableWorkersToRecruit.Count);
                colony.queen.CreateAnt((AntWorker)colony.queen.aviableWorkersToRecruit[id], colony);
            }

            for (int i = 0; i < colony.startPopulation["warriors"]; i++)
            {
                int id = rand.Next(0, colony.queen.aviableWarriorsToRecruit.Count);
                colony.queen.CreateAnt((AntWarrior)colony.queen.aviableWarriorsToRecruit[id], colony);
            }

            colony.population.Add(colony.queen.aviableUniqueAntToRecruit,colony);
            greenColony.population.uniqueAnt = null;

        }
        
    }


    class Colony
    {
        public string colonyName;
        private static int _globalColonyNumber = 0;
        public int colonyNumber;
        public Queen queen;
        public Dictionary<string, int> startPopulation;
        public AntPopulation population = new AntPopulation();
        public AntPopulation diesAnts = new AntPopulation();
        public AntPopulation newAnts = new AntPopulation();
        public Resources resources = new Resources(0, 0, 0, 0);
        public Resources thisDayResources = new Resources(0, 0, 0, 0);

        public Colony(string colonyName, Queen queen, Dictionary<string, int> startPopulation)
        {
            this.colonyName = colonyName;
            this.queen = queen;
            this.startPopulation = startPopulation;
            _globalColonyNumber++;
            colonyNumber = _globalColonyNumber;
        }

        public void TryBornLarvas()
        {
            for (int i = 0; i < queen.currentLarvas.Count; i++)
            {
                if (queen.currentLarvas[i] != null)
                {
                    if (queen.currentLarvas[i].daysToBorn <= 0)
                    {
                        if (queen.currentLarvas[i].GetType() == typeof(UniqueAnt))
                        {
                            Console.WriteLine($"UniqueAnt {colonyName} {colonyNumber}------------------------------------------------1111111111111111111111111111111");
                        }
                        population.Add((Ant)queen.currentLarvas[i].antType.Clone(this), this);
                        newAnts.Add((Ant)queen.currentLarvas[i].antType.Clone(this), this);
                        queen.currentLarvas.Remove(queen.currentLarvas[i]);
                    }
                    else
                    {
                        queen.currentLarvas[i].daysToBorn--;
                    }
                }
            }

            if (queen.queenLarva != null)
            {
                if (queen.queenLarva.daysToBorn <= 0)
                {
                    Process.colonies.Add(this.Clone());
                    queen.queenLarva = null;

                    Process.InitializeAnts(Process.colonies[Process.colonies.Count - 1]);
                }
                else
                {
                    queen.queenLarva.daysToBorn--;
                }
            }

        }

        public void ShowInfo()
        {
            Console.WriteLine("Экран 3 - Информация о колонии");
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Колония {colonyName} {colonyNumber}");
            Console.WriteLine($"--- Королева <{queen.name}>: здоровье={queen.health}, защита={queen.defence}," +
                              $"урон={queen.damage}");
            resources.ShowValues();
            Console.WriteLine("<<<<<<<<<<<<<Рабочие>>>>>>>>>>>>>");
            for (int i = 0; i < queen.aviableWorkersToRecruit.Count; i++)
            {
                int countType = 0;
                Console.WriteLine($"Тип: {queen.aviableWorkersToRecruit[i].type}");
                Console.WriteLine($"Параметры: здоровье={queen.aviableWorkersToRecruit[i].health}, защита={queen.aviableWorkersToRecruit[i].defence}");
                for (int j = 0; j < population.antWorkersPopulation.Count; j++)
                {
                    if (population.antWorkersPopulation[j].type == queen.aviableWorkersToRecruit[i].type)
                    {
                        countType++;
                    }
                }
                Console.WriteLine($"Количество: {countType}");
            }
            
            Console.WriteLine("<<<<<<<<<<<<<Воины>>>>>>>>>>>>>");
            for (int i = 0; i < queen.aviableWarriorsToRecruit.Count; i++)
            {
                int countType = 0;
                Console.WriteLine($"Тип: {queen.aviableWarriorsToRecruit[i].type}");
                Console.WriteLine($"Параметры: здоровье={queen.aviableWarriorsToRecruit[i].health}, защита={queen.aviableWarriorsToRecruit[i].defence}, урон={queen.aviableWarriorsToRecruit[i].damage}");
                for (int j = 0; j < population.antWarriorsPopulation.Count; j++)
                {
                    if (population.antWarriorsPopulation[j].type == queen.aviableWarriorsToRecruit[i].type)
                    {
                        countType++;
                    }
                }
                Console.WriteLine($"Количество: {countType}");
            }
            Console.WriteLine("<<<<<<<<<<<<<Особые>>>>>>>>>>>>>");
            if (population.uniqueAnt != null)
	        {
                Console.WriteLine($"Тип: {population.uniqueAnt.type}");
                Console.WriteLine($"Параметры: здоровье={population.uniqueAnt.health}, защита={population.uniqueAnt.defence}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Особые насекомые отсутсвуют");
            }
            
            

        }

        public Colony Clone()
        {
            return new Colony(this.colonyName,queen.Clone(), new Dictionary<string, int>(this.startPopulation));
        }
    }
    
    class Queen
    {
        public string name;
        public int health;
        public int damage;
        public int defence;
        public int minLarvaGrowCycle;
        public int maxLarvaGrowCycle;
        public int minQueens;
        public int maxQueens;
        public int queenLimit;
        
        public List<AntWorker> aviableWorkersToRecruit;
        public List<AntWarrior> aviableWarriorsToRecruit;
        public UniqueAnt aviableUniqueAntToRecruit;
        public List<Larva> currentLarvas = new List<Larva>();
        public QueenLarva queenLarva;
        public static int queenID = 0;
        

        public Queen(string name, int health, int damage, int defence, int minLarvaGrowCycle, int maxLarvaGrowCycle,
            int minQueens, int maxQueens)
        {
            this.health = health;
            this.damage = damage;
            this.defence = defence;
            this.minLarvaGrowCycle = minLarvaGrowCycle;
            this.maxLarvaGrowCycle = maxLarvaGrowCycle;
            this.minQueens = minQueens;
            this.maxQueens = maxQueens;
            this.name = name;
            queenLimit = Process.rand.Next(maxQueens, maxQueens + 1);
        }

        public void CreateAnt(Ant ant, Colony colony)
        {
            colony.population.Add((Ant)ant, colony);
        }

        public void CreateLarvas(int countLarvas)
        {
            int chance = Process.rand.Next(0, 101);
            for (int i = 0; i < countLarvas; i++)
            {
                chance = Process.rand.Next(0, 101);
                if (chance <= 40)
                {
                    int antID = Process.rand.Next(0, aviableWorkersToRecruit.Count);
                    currentLarvas.Add(new Larva(aviableWorkersToRecruit[antID],
                        Process.rand.Next(minLarvaGrowCycle,maxLarvaGrowCycle+1)));
                }
                else if (chance <= 96)
                {
                    int antID = Process.rand.Next(0, aviableWarriorsToRecruit.Count);
                    currentLarvas.Add(new Larva(aviableWarriorsToRecruit[antID],
                        Process.rand.Next(minLarvaGrowCycle,maxLarvaGrowCycle+1)));
                }
                else
                {
                    if (queenLimit > 0)
                    {
                        queenLarva = new QueenLarva(this.Clone(),Process.rand.Next(minLarvaGrowCycle,maxLarvaGrowCycle+1));
                        queenID++;
                        queenLimit--;
                    }
                    else
                    {
                        i--;
                    }
                    
                }
            }
            currentLarvas.Add(new Larva(aviableUniqueAntToRecruit,Process.rand.Next(minLarvaGrowCycle,maxLarvaGrowCycle+1)));
            
        }

        public Queen Clone()
        {
            Queen queen = new Queen(this.name, this.health, this.damage, this.defence, this.minLarvaGrowCycle,
                this.maxLarvaGrowCycle, 0, 0);
            queen.aviableWorkersToRecruit = new List<AntWorker>(this.aviableWorkersToRecruit);
            queen.aviableWarriorsToRecruit = new List<AntWarrior>(this.aviableWarriorsToRecruit);
            queen.aviableUniqueAntToRecruit = this.aviableUniqueAntToRecruit;
            return queen;
        }
    }
    class Larva
    {
        public Ant antType;
        public int daysToBorn;

        public Larva(Ant antType, int daysToBorn)
        {
            this.antType = antType;
            this.daysToBorn = daysToBorn;
        }
    }

    class QueenLarva
    {
        public Queen queen;
        public int daysToBorn;

        public QueenLarva(Queen queen, int daysToBorn)
        {
            this.queen = queen;
            this.daysToBorn = daysToBorn;
        }
    }

    class AntPopulation
    {
        public List<AntWorker> antWorkersPopulation = new List<AntWorker>();
        public List<AntWarrior> antWarriorsPopulation = new List<AntWarrior>();

        public UniqueAnt uniqueAnt;

        public void Add(Ant ant, Colony colony)
        {
            if (ant.GetType() == typeof(AntWorker))
            {
                AntWorker antWorker = (AntWorker) ant.Clone(colony);
                antWorkersPopulation.Add(antWorker);
            }
            else if (ant.GetType() == typeof(AntWarrior))
            {
                antWarriorsPopulation.Add((AntWarrior) ant.Clone(colony));
            }
            else if (ant.GetType() == typeof(UniqueAnt))
            {
                uniqueAnt = (UniqueAnt)ant.Clone(colony);
            }


        }
        public void Remove(Ant ant)
        {
            if (ant.GetType() == typeof(AntWorker))
            {
                antWorkersPopulation.Remove((AntWorker)ant);
            }
            else if (ant.GetType() == typeof(AntWarrior))
            {
                antWarriorsPopulation.Remove((AntWarrior)ant);
            }
            else if (ant.GetType() == typeof(UniqueAnt))
            {
                uniqueAnt = null;
            }
            
        }
        public void ClearAll()
        {
            antWorkersPopulation.Clear();
            antWarriorsPopulation.Clear();
            uniqueAnt = null;
        }
    }
    class Resources
    {
        public int stones;
        public int leaves;
        public int sticks;
        public int dewdrops;

        public Resources(int stones, int leaves, int sticks, int dewdrops)
        {
            this.stones = stones;
            this.leaves = leaves;
            this.sticks = sticks;
            this.dewdrops = dewdrops;
        }

        public void AddValues(int stones, int leaves, int sticks, int dewdrops)
        {
            this.stones += stones;
            this.leaves += leaves;
            this.sticks += sticks;
            this.dewdrops += dewdrops;
        }
        
        public void RemoveValues(int stones, int leaves, int sticks, int dewdrops)
        {
            this.stones -= stones;
            this.leaves -= leaves;
            this.sticks -= sticks;
            this.dewdrops -= dewdrops;
        }

        public void ClearValues()
        {
            this.stones = 0;
            this.leaves = 0;
            this.sticks = 0;
            this.dewdrops = 0;
        }

        public int SummAllResources()
        {
            return stones + sticks + leaves + dewdrops;
        }

        public void ShowValues()
        {
            Console.Write($"к={stones} ");
            Console.Write($"л={leaves} ");
            Console.Write($"в={sticks} ");
            Console.WriteLine($"р={dewdrops}");
        }
    }

    class Heap
    {
        public Resources resources;
        public List<Ant> visitors = new List<Ant>();
        public List<string> colonyNames = new List<string>();
        public bool allowModificators = true;

        public Heap(Resources resources)
        {
            this.resources = resources;
        }

        public void RemoveResource(EResources resourceToRemove, int count)
        {
            switch (resourceToRemove)
            {
                case EResources.Stones:
                    resources.stones -= count;
                    break;
                case EResources.Sticks:
                    resources.sticks -= count;
                    break;
                case EResources.Dewdrops:
                    resources.dewdrops -= count;
                    break;
                case EResources.Leaves:
                    resources.leaves -= count;
                    break;
            }
        }

        public void Fight()
        {
            for (int i = 0; i < visitors.Count; i++)
            {
                if (visitors[i].GetType() == typeof(AntWarrior))
                {
                    ((AntWarrior)visitors[i]).Attack(this);
                }
            }
        }
    }

    abstract class Ant
    {
        public int health;
        public int startHealth;
        public int defence; 
        public string colonyName;
        public List<Specials> specials;
        public Colony mainColony;
        public Heap currentHeap;
        public string type;

        public Ant(int health, int defence, string colonyName, List<Specials> specials, string type)
        {
            this.defence = defence;
            this.colonyName = colonyName;
            this.specials = specials;
            this.health = health + defence;
            this.type = type;
            startHealth = health;
        }

        public abstract void Duel(Ant attcker);
        public abstract void Damage(int value);

        public abstract void Trip();

        public abstract void BackToColony();
        public abstract void Speak();

        public abstract object Clone(Colony colony);
    }

    class AntWorker : Ant
    {
        public List<EResources> availableResources = new List<EResources>(); //Change to private
        private List<EResources> resourcesToTake = new List<EResources>();
        public Resources inventory;
        public int maxWeight;

        public AntWorker(int health, int defence, string colonyName, List<EResources> availableResources, int maxWeight,
            Resources inventory, List<Specials> specials, string type) : base(health, defence, colonyName, specials, type)
        {
            this.availableResources = availableResources;
            this.maxWeight = maxWeight;
            this.inventory = inventory;
        }

        public void TakeResource(Heap heap)
        {
            for (int i = 0; i < availableResources.Count; i++)
            {
                resourcesToTake.Add(availableResources[i]);
            }
            

            for (int i = 0; i < maxWeight; i++)
            {
                int id = Process.rand.Next(0, resourcesToTake.Count);
                bool flag = true;

                switch (resourcesToTake[id])
                {
                    case EResources.Stones:
                        if (heap.resources.stones > 0)
                        {
                            inventory.stones++;
                        }
                        else
                        {
                            flag = false;
                        }

                        break;
                    case EResources.Sticks:
                        if (heap.resources.sticks > 0)
                        {
                            inventory.sticks++;
                        }
                        else
                        {
                            flag = false;
                        }

                        break;
                    case EResources.Dewdrops:
                        if (heap.resources.dewdrops > 0)
                        {
                            inventory.dewdrops++;
                        }
                        else
                        {
                            flag = false;
                        }

                        break;
                    case EResources.Leaves:
                        if (heap.resources.leaves > 0)
                        {
                            inventory.leaves++;
                        }
                        else
                        {
                            flag = false;
                        }

                        break;
                }

                if (flag)
                {
                    heap.RemoveResource(resourcesToTake[id], 1);
                }

                resourcesToTake.Remove(resourcesToTake[id]);
            }
        }

        public void ShowInventory()
        {
            inventory.ShowValues();
        }

        public override void Duel(Ant attcker)
        {
            health = 0;
        }

        public override void Damage(int value)
        {
            health -= value;
        }

        public override void Trip()
        {
            int heapID = Process.rand.Next(0, Process.heaps.Count);

            currentHeap = Process.heaps[heapID];
            if (currentHeap.allowModificators)
            {
                for (int i = 0; i < specials.Count; i++)
                {
                    if (specials[i] == Specials.Stupid)
                    {
                        int chanceToDisappear = Process.rand.Next(0, 2);
                        switch (chanceToDisappear)
                        {
                            case 0:
                                health = 0;
                                break;
                            case 1:
                                break;
                        }
                    }

                    if (specials[i] == Specials.QueenFavorite)
                    {
                        for (int j = 0; j < Process.heaps.Count; j++)
                        {
                            bool flag = true;
                            for (int k = 0; k < Process.heaps[i].colonyNames.Count; k++)
                            {
                                if ((Process.heaps[i].colonyNames[i] != this.colonyName) && (Process.heaps[i].colonyNames.Count > 0))
                                {
                                    flag = false;
                                    break;
                                }
                            }

                            if (flag)
                            {
                                currentHeap = Process.heaps[i];
                            }
                        
                        }
                    }

                    if (specials[i] == Specials.Veteran)
                    {
                        currentHeap.allowModificators = false;
                    }
                }
            }
            

            currentHeap.visitors.Add(this);
            if (!currentHeap.colonyNames.Contains(this.colonyName))
            {
                currentHeap.colonyNames.Add(this.colonyName);
            }

            this.TakeResource(currentHeap);
        }

        public override void BackToColony()
        {
            mainColony.resources.AddValues(inventory.stones,inventory.leaves, inventory.sticks,inventory.dewdrops);
            mainColony.thisDayResources.AddValues(inventory.stones, inventory.leaves, inventory.sticks, inventory.dewdrops);
            inventory.ClearValues();
            currentHeap = null;
        }

        public override void Speak()
        {
            Console.WriteLine($"Я рабочий из колонии {mainColony.colonyName}, " +
                              $"у меня сейчас {inventory.SummAllResources()} ресурсов," +
                              $"{health} здоровья и {defence} защиты, мою королеву зовут {mainColony.queen.name}");
        }

        public override object Clone(Colony colony)
        {
            AntWorker antWorker = new AntWorker(this.health, this.defence, this.colonyName,
                new List<EResources>(this.availableResources),
                this.maxWeight, this.inventory, new List<Specials>(this.specials), type);
            antWorker.mainColony = colony;
            return antWorker;
        }
    }

    class AntWarrior : Ant
    {
        public int damage;
        public int maxTargets;
        public int attacks;

        public AntWarrior(int health, int defence, int damage, int attacks, int maxTargets, string colonyName,
            List<Specials> specials, string type) : base(health, defence, colonyName, specials, type)
        {
            this.damage = damage;
            this.attacks = attacks;
            this.maxTargets = maxTargets;
        }

        public void Attack(Heap currentHeap)
        {
            int targetID = -1;

            for (int i = 0; i < currentHeap.visitors.Count; i++)
            {
                if (currentHeap.visitors[i].colonyName != this.colonyName && currentHeap.visitors[i].health > 0 && !currentHeap.visitors[i].specials.Contains(Specials.GodMode))
                {
                    targetID = currentHeap.visitors.IndexOf(currentHeap.visitors[i]);
                    break;
                }
            }

            if (targetID >= 0)
            {
                currentHeap.visitors[targetID].Duel(this);
            }
        }


        public override void Duel(Ant attcker)
        {
            if (attcker.GetType() == typeof(AntWarrior))
            {
                AntWarrior warrior = (AntWarrior)attcker;
                if (!this.specials.Contains(Specials.Mythic))
                {
                    for (int i = 0; i < warrior.attacks; i++)
                    {
                        this.Damage(warrior.damage);
                    }

                    for (int i = 0; i < this.attacks; i++)
                    {
                        warrior.Damage(this.damage);
                    }
                }
                else
                {
                    attcker.Damage(attcker.health);
                }
            }
            if (attcker.GetType() == typeof(UniqueAnt))
            {
                UniqueAnt uniqueAnt = (UniqueAnt)attcker;
                if (!this.specials.Contains(Specials.Mythic))
                {
                    for (int i = 0; i < uniqueAnt.attacks; i++)
                    {
                        this.Damage(uniqueAnt.damage);
                    }

                    for (int i = 0; i < this.attacks; i++)
                    {
                        uniqueAnt.Damage(this.damage);
                    }
                }
                else
                {
                    attcker.Damage(attcker.health);
                }
            }
        }

        public override void Damage(int value)
        {
            health -= value;
        }

        public override void Trip()
        {
            int heapID = Process.rand.Next(0, Process.heaps.Count);

            currentHeap = Process.heaps[heapID];

            if (currentHeap.allowModificators)
            {
                for (int i = 0; i < specials.Count; i++)
                {
                    if (specials.Contains(Specials.Mythic))
                    {
                        currentHeap.allowModificators = false;
                    }
                }
            }
            

            currentHeap.visitors.Add(this);
            if (!currentHeap.colonyNames.Contains(this.colonyName))
            {
                currentHeap.colonyNames.Add(this.colonyName);
            }
        }

        public override void BackToColony()
        {
            currentHeap = null;
        }

        public override void Speak()
        {
            Console.WriteLine($"Я воин из колонии {mainColony.colonyName}, " +
                              $"у меня {health} здоровья, {damage} урона и {defence} защиты, мою королеву зовут {mainColony.queen.name}");
        }

        public override object Clone(Colony colony)
        {
            AntWarrior antWarrior = new AntWarrior(this.health, this.defence, this.damage, this.attacks, this.maxTargets,this.colonyName, new List<Specials>(this.specials), this.type);
            antWarrior.mainColony = colony;
            return antWarrior;
        }
    }

    class UniqueAnt : Ant
    {
        public int damage;
        public int maxTargets;
        public int attacks;

        public UniqueAnt(int health, int defence, string colonyName, List<Specials> specials, string type) : base(health, defence,
            colonyName, specials, type)
        {
        }

        public override void Duel(Ant attcker)
        {
            if (attcker.GetType() == typeof(AntWarrior))
            {
                AntWarrior warrior = (AntWarrior)attcker;
                for (int i = 0; i < warrior.attacks; i++)
                {
                    this.Damage(warrior.damage);
                }

                for (int i = 0; i < this.attacks; i++)
                {
                    warrior.Damage(this.damage);
                }
            }
        }

        public override void Damage(int value)
        {
            health -= value;
        }

        public override void Trip()
        {
            int heapID = Process.rand.Next(0, Process.heaps.Count);

            currentHeap = Process.heaps[heapID];

            for (int i = 0; i < specials.Count; i++)
            {
                if (specials[i] == Specials.Epic)
                {
                    for (int j = 0; j < currentHeap.visitors.Count; j++)
                    {
                        Ant currentAnt = currentHeap.visitors[j];
                        if (currentAnt.colonyName == this.colonyName)
                        {
                            currentAnt.defence *= 2;
                            currentAnt.health *= 2;
                        }
                        
                    }
                }

                if (specials[i] == Specials.Suspectful)
                {
                    currentHeap.allowModificators = false;
                }
            }

            currentHeap.visitors.Add(this);
            if (!currentHeap.colonyNames.Contains(this.colonyName))
            {
                currentHeap.colonyNames.Add(this.colonyName);
            }
        }

        public override void BackToColony()
        {
            if (specials.Contains(Specials.Epic) )
            {
                for (int j = 0; j < currentHeap.visitors.Count; j++)
                {
                    Ant currentAnt = currentHeap.visitors[j];
                    if (currentAnt.colonyName == this.colonyName)
                    {
                        currentAnt.defence /= 2;
                        currentAnt.health /= 2;
                    }

                    if (currentAnt.health == 0)
                    {
                        currentAnt.health = 1;
                    }
                        
                }
            }
            currentHeap = null;
        }

        public override void Speak()
        {
            Console.WriteLine($"Я особый из колонии {mainColony.colonyName}, " +
                              $"у меня {health} здоровья, {damage} урона и {defence} защиты, мою королеву зовут {mainColony.queen.name}");
        }

        public override object Clone(Colony colony)
        {
            UniqueAnt uniqueAnt = new UniqueAnt(this.health,this.defence,this.colonyName,new List<Specials>(this.specials), type);
            uniqueAnt.mainColony = colony;

            return uniqueAnt;
        }
    }
}