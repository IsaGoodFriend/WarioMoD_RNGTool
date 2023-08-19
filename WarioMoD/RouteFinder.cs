using System.Reflection;
using System.Collections;
using WarioMoD.RNG;
using System.Text.RegularExpressions;


namespace WarioMoD {
	public class RouteSection {
		public RouteSection(int amount, int cost, int index) {

			Index = index;

			AmountOfThing = amount;
			Cost = cost;
		}
		public RNGState StartingSeed, EndingSeed;
		public int AmountOfThing;

		public int Index;

		public int Cost;

		public string FriendlyName;

		public RouteSection? nextPart, lastPart;
	}
	public struct RNGState {
		public long seed;
		public RedChestType pool, previous;
		public int traceCount, level;

		public override string ToString() {
			return seed.ToString("x16");
		}
	}
	public class StraightShot {
		static MethodInfo[] methods;

		static StraightShot() {

			methods = new MethodInfo[] {
					((Delegate)ConsistentRNG).Method,
					((Delegate)RedChest).Method,
					((Delegate)GreenChest).Method,
					((Delegate)LeaveLevel).Method,
					((Delegate)EnterLevel).Method,
					((Delegate)SphinxPuzzle).Method,
				};
		}

		static int ConsistentRNG(RouteFinder baseFinder, int rngCalls) {

			for (int i = 0; i < rngCalls; i++)
				baseFinder.random.GetInteger();

			return 0;
		}
		static int LeaveLevel(RouteFinder baseFinder) {

			baseFinder.Pool = RedChestType.None;
			baseFinder.Previous = RedChestType.None;
			baseFinder.TraceDrought = 0;

			return 0;
		}
		static int EnterLevel(RouteFinder baseFinder, int level) {

			baseFinder.CurrentLevel = level;

			return 0;
		}
		static int SphinxPuzzle(RouteFinder baseFinder, int version, bool addCost) {

			List<int> list = new List<int>();

			int bits = 0;

			int count = 0;

			while (count < 5) {
				int value = baseFinder.random.GetRange(20);

				int idx = value;

				value = 1 << value;
				if ((value & bits) != 0)
					continue;

				if (count == version || (count > 2 && version == 2)) {
					list.Add(idx);
				}

				bits |= value;

				count++;
			}

			if (!addCost)
				return 0;

			int cost = 0;

			foreach (var val in list) {
				switch (val) {
					case 11: // ant
						cost += 0;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Ant");
						break;
					case 5: // dog
						cost += 51;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Dog");
						break;
					case 16: // pear
						cost += 59;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Pear");
						break;
					case 12: // fly
						cost += 72;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Fly");
						break;
					case 8: // frog
						cost += 92;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Frog");
						break;
					case 18: // comb
						cost += 95;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Comb");
						break;
					case 1: // robin
						cost += 115;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Robin");
						break;
					case 4: // bear
						cost += 116;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Bear");
						break;
					case 7: // man
						cost += 123;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Man");
						break;
					case 6: // ray
						cost += 132;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Ray");
						break;
					case 14: // soap
						cost += 134;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Soap");
						break;
					case 13: // apple
						cost += 148;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Apple");
						break;
					case 0: // crane
						cost += 157;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Crane");
						break;
					case 3: // chick
						cost += 190;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Chick");
						break;
					case 10: // spider
						cost += 261;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Spider");
						break;
					case 9: // lizard
						cost += 288;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Lizard");
						break;
					case 2: // penguin
						cost += 293;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Penguin");
						break;
					case 15: // rainbow
						cost += 293;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Rainbow");
						break;
					case 17: // glasses
						cost += 332;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Glasses");
						break;
					case 19: // calendar
						cost += 337;
						if (RouteFinder.Exporting)
							RouteFinder.SphynxAnswers.Add("Calendar");
						break;
				}
			}

			return cost;
		}
		static int RedChest(RouteFinder baseFinder) {

			var chest = new RedPurpleChest(baseFinder);

			if (chest.currentType == RedChestType.Match5)
				return 100000;

			return chest.Cost;
		}
		static int GreenChest(RouteFinder baseFinder, GreenChestType type, bool upgrade) {

			var chest = new GreenChest(type, upgrade, baseFinder.random);

			return chest.Cost;
		}

		public static StraightShot[] GetRoute(string filePath) {

			List<StraightShot> routePoints = new List<StraightShot>();

			using (StreamReader reader = new StreamReader(File.OpenRead(filePath))) {
				StraightShot temp;
				do {
					temp = new StraightShot(reader);

					if (!temp.IsEmpty)
						routePoints.Add(temp);

				} while (!temp.IsEmpty && !reader.EndOfStream);
			}

			return routePoints.ToArray();
		}

		public string FriendlyName { get; private set; }

		int[] methodsToRun;
		object[][] methodParameters;

		int[] extraCalls, extraCost;
		int minimumCalls;

		public bool IsEmpty => methodsToRun.Length == 0;

		public StraightShot(StreamReader reader) {

			string? getline() {

				string s;
				do {
					if (reader.EndOfStream)
						return null;

					s = reader.ReadLine()!;
					if (s == null)
						return null;

					s = Regex.Replace(s, @"//.+", "").Trim();
				}
				while (string.IsNullOrWhiteSpace(s));

				return s;
			}

			List<Delegate> actions = new List<Delegate>();
			List<object[]> args = new List<object[]>();
			List<int> extra = new List<int>(),
				costExtra = new List<int>();

			int constantRNGCalls = 0;
			void addRNGCalls() {
				if (constantRNGCalls <= 0)
					return;

				actions.Add(ConsistentRNG);

				object[] p = new object[]{
						constantRNGCalls,
					};

				args.Add(p);
				constantRNGCalls = 0;
			}


			string input;
			string friendlyName = "";

			while ((input = getline()!) != null) {
				friendlyName = "";
				Match match = Regex.Match(input, @"\[([\w ]+)\]");
				if (match.Success)
					friendlyName = match.Groups[1].Value;

				bool continueLoop = true;
				match = Regex.Match(input, @"(\w+) *(?:\( *([\w-, ]+) *\))?(.*)");

				string cost = Regex.Match(match.Groups[3].Value, @"\(([\d-]+)\)").Groups[1].Value;
				int costMul = 0, costAdd = 0;
				if (cost != "") {
					try {

						string[] split = cost.Split(':');
						costMul = int.Parse(split[0]);
						costAdd = int.Parse(split[1]);
					}
					catch (Exception e) {
						costMul = 0;
						costAdd = 0;
					}
				}

				void AddCosts(int amount) {
					for (int i = 0; i < amount; i++) {
						int am = costMul * i + costAdd;
						if (am < 0)
							am = 0;

						costExtra.Add(am);
					}
				}

				switch (match.Groups[1].Value) {
					case "GreenChest":
					case "GreenChestUpgrade": {

						addRNGCalls();
						GreenChestType type = GreenChestType.Captain;

						switch (match.Groups[2].Value) {
							case "Cosmic":
							case "Space":
							case "Astronaut":
							case "Gun":
								type = GreenChestType.Cosmic;
								break;
							case "Paint":
							case "Painter":
							case "Arty":
							case "Art":
								type = GreenChestType.Arty;
								break;
							case "Devil":
							case "Evil":
							case "Wicked":
							case "Flying":
							case "Fly":
								type = GreenChestType.Devil;
								break;
							case "Ship":
							case "Captain":
							case "Boat":
							case "Sub":
							case "Submarine":
								type = GreenChestType.Captain;
								break;
							case "Genius":
							case "Smart":
							case "Clever":
							case "Water":
								type = GreenChestType.Genius;
								break;
							case "Lightning":
							case "Spark":
							case "Sparky":
								type = GreenChestType.Sparky;
								break;
							case "Dragon":
							case "Fire":
							case "Furry":
								type = GreenChestType.Dragon;
								break;
						}

						actions.Add(GreenChest);
						args.Add(new object[]{
								type,
								match.Groups[1].Value == "GreenChestUpgrade"
							});
					}
					break;
					case "RedChest":
					case "PurpleChest": {

						addRNGCalls();

						actions.Add(RedChest);
						args.Add(new object[0]);
					}
					break;
					case "Chapter2Elevator": {

						constantRNGCalls += 1;
					}
					break;
					case "Chapter3FirstRoom": {

						addRNGCalls();

						actions.Add(SphinxPuzzle);
						args.Add(new object[] {
								0, false,
							});
					}
					break;
					case "Chapter10SpikeRoom": {

						constantRNGCalls += 1;
					}
					break;
					case "SphinxSim":
					case "SphinxSimulator": {

						addRNGCalls();

						actions.Add(SphinxPuzzle);
						args.Add(new object[] {
								int.Parse(match.Groups[2].Value),
								true,
							});
					}
					break;
					case "Sphinx": {

						addRNGCalls();

						actions.Add(SphinxPuzzle);
						args.Add(new object[] {
								2,
								true,
							});
					}
					break;
					case "BulletHit":
					case "UpgradedBulletShots": {

						int mult = match.Groups[1].Value == "BulletHit" ? 9 : 36;

						if (match.Groups[2].Value.Contains('-')) {
							match = Regex.Match(match.Groups[2].Value, @"(\d+) *- *(\d+)");

							int min = int.Parse(match.Groups[1].Value);
							int max = int.Parse(match.Groups[2].Value) - min;

							if (min < 0 || max < 0)
								throw new Exception();

							constantRNGCalls += min * mult;

							if (max > 0) {
								addRNGCalls();

								if (costMul > 0 && costAdd != 0) {
									AddCosts(max);
								}

								minimumCalls = min;
								for (int i = 0; i < max; i++) {
									extra.Add(i * mult);
								}

								continueLoop = false;
							}

						}
						else {
							constantRNGCalls += int.Parse(match.Groups[2].Value) * mult;
						}


					}
					break;
					case "StartNewFile": {
						constantRNGCalls += 1;

						if (RouteFinder.HumanRunning)
							constantRNGCalls += 12;
						else
							constantRNGCalls += 6;
					}
					break;
					case "CompleteLevel": {
						if (RouteFinder.HumanRunning) {
							constantRNGCalls += 117;
							addRNGCalls();
						}
						else {
							match = Regex.Match(match.Groups[2].Value, @"(\d+) *- *(\d+)");

							int min = 0;
							int max = 30;

							constantRNGCalls += 1;

							addRNGCalls();

							minimumCalls = min;
							for (int i = 0; i < max; i++) {
								extra.Add(i);
								costExtra.Add(i);
							}

							continueLoop = false;
						}

						actions.Add(LeaveLevel);
						args.Add(new object[0]);
					}
					break;
					case "LeaveLevel": {
						constantRNGCalls += 117;

						addRNGCalls();

						actions.Add(LeaveLevel);
						args.Add(new object[0]);
					}
					break;
					case "SelectLevel": {

						constantRNGCalls += 64;

						actions.Add(EnterLevel);
						args.Add(new object[] {
							int.Parse(match.Groups[2].Value)
						});

					}
					break;
					case "FileLength": {

						if (match.Groups[2].Value.Contains('-')) {
							match = Regex.Match(match.Groups[2].Value, @"(\d+) *- *(\d+)");

							int min = int.Parse(match.Groups[1].Value);
							int max = int.Parse(match.Groups[2].Value) - min;

							if (min < 1)
								throw new Exception();

							constantRNGCalls += min;
							addRNGCalls();

							minimumCalls = min;
							for (int i = 0; i < max; i++) {
								extra.Add(i);
								costExtra.Add(i * 5);
							}

							continueLoop = false;
						}
						else {
							constantRNGCalls += int.Parse(match.Groups[2].Value);
						}
					}
					break;
				}
				if (!continueLoop || extra.Count > 0)
					break;

			}
			addRNGCalls();

			methodsToRun = new int[actions.Count];
			for (int i = 0; i < actions.Count; i++) {
				methodsToRun[i] = Array.IndexOf(methods, actions[i].Method);
			}
			methodParameters = args.ToArray();

			FriendlyName = friendlyName;

			if (extra.Count == 0)
				extra.Add(0);
			extraCalls = extra.ToArray();

			while (extra.Count > costExtra.Count) {
				if (costExtra.Count == 0)
					costExtra.Add(0);
				else
					costExtra.Add(costExtra[costExtra.Count - 1]);
			}
			extraCost = costExtra.ToArray();
		}

		public void Run(RouteFinder baseFinder, int index) {
			int cost = 0;

			for (int i = 0; i < methodsToRun.Length; i++) {
				List<object> a = new List<object> {
						baseFinder
					};
				a.AddRange(methodParameters[i]);
				cost += (int)methods[methodsToRun[i]].Invoke(null, a.ToArray())!;
			}

			baseFinder.SaveState();
			for (int i = 0; i < extraCalls.Length; i++) {
				baseFinder.ReloadState();

				for (int j = 0; j < extraCalls[i]; j++)
					baseFinder.random.GetInteger();

				baseFinder.AddSeed(i + minimumCalls, cost + extraCost[i], index);
			}
			baseFinder.PopState();
		}
		public IEnumerable GetChests(RouteFinder baseFinder, int rngCalls) {
			int cost = 0;

			for (int i = 0; i < methodsToRun.Length; i++) {
				if (methods[methodsToRun[i]] == ((Delegate)GreenChest).Method) {
					yield return new GreenChest((GreenChestType)methodParameters[i][0], (bool)methodParameters[i][1], baseFinder.random);
				}
				else if (methods[methodsToRun[i]] == ((Delegate)RedChest).Method) {

					yield return new RedPurpleChest(baseFinder);
				}
				else {

					var curr = methods[methodsToRun[i]];

					List<object> a = new List<object> {
						baseFinder
					};
					a.AddRange(methodParameters[i]);
					cost += (int)methods[methodsToRun[i]].Invoke(null, a.ToArray())!;
				}
			}

			for (int j = 0; j < extraCalls[rngCalls - minimumCalls]; j++)
				baseFinder.random.GetInteger();
		}
	}

	public class RouteFinder {

		public static bool HumanRunning = true;
		public static bool Exporting = false;

		public static List<string> SphynxAnswers = new List<string>();


		StraightShot[] routes;

		Stack<RNGState> States = new Stack<RNGState>();
		RNGState startState;

		List<RNGState> currentSeedPile = new List<RNGState>();
		List<List<RNGState>> endingSeeds = new List<List<RNGState>>();

		Dictionary<RNGState, List<int>> routeLookup = new Dictionary<RNGState, List<int>>();
		List<RouteSection> routeSections = new List<RouteSection>();

		long rootSeed;


		public int CurrentLevel = 0;

		public RedChestType Previous = RedChestType.None;
		public RedChestType Pool = RedChestType.None;
		public int TraceDrought = 0;

		private int[] sectionSOB;

		public WarioRandom random;

		public RouteFinder(long startingSeed, StraightShot[] route) {
			random = new WarioRandom(startingSeed);
			rootSeed = startingSeed;

			sectionSOB = new int[route.Length];

			for (int i = 0; i < sectionSOB.Length; i++) {
				sectionSOB[i] = int.MaxValue;
			}

			routes = route;
		}

		public void SaveState() {
			States.Push(GetCurrentState());
		}
		public void ReloadState() {
			SetCurrentState(States.Peek());
		}
		public void PopState() {
			States.Pop();
		}
		public void AddSeed(int amount, int cost, int index) {
			var state = GetCurrentState();

			if (!currentSeedPile.Contains(state))
				currentSeedPile.Add(state);

			var route = new RouteSection(amount, cost, index);
			route.FriendlyName = routes[index].FriendlyName;

			route.StartingSeed = startState;
			route.EndingSeed = state;

			if (route.Cost > 0)
				sectionSOB[route.Index] = Math.Min(sectionSOB[route.Index], route.Cost);

			routeSections.Add(route);
			if (!routeLookup.ContainsKey(route.StartingSeed)) {
				routeLookup[route.StartingSeed] = new List<int>();
			}
			routeLookup[route.StartingSeed].Add(routeSections.Count - 1);
		}

		private RNGState GetCurrentState() {
			return new RNGState() {
				pool = Pool,
				previous = Previous,
				traceCount = TraceDrought,
				level = CurrentLevel,

				seed = random.Seed,
			};
		}
		private void SetCurrentState(RNGState state) {
			Pool = state.pool;
			Previous = state.previous;
			TraceDrought = state.traceCount;
			CurrentLevel = state.level;

			random.Seed = state.seed;
		}

		public static int BestCost;

		static object bestLock = new object();
		static bool SetNewBest(int testCost) {

			lock (bestLock) {
				if (testCost < BestCost) {
					BestCost = testCost;
					return true;
				}
				return false;
			}
		}


		public RouteSection? Run(bool gameInit) {
			for (int i = 0; i < routes.Length; i++) {
				endingSeeds.Add(new List<RNGState>());
			}

			for (int i = 0; i < routes.Length; i++) {
				currentSeedPile = endingSeeds[i];
				currentSeedPile.Clear();

				if (i == 0) {
					SetCurrentState(new RNGState() { seed = rootSeed });
					startState = GetCurrentState();

					if (gameInit)
						random.GetInteger();

					routes[i].Run(this, 0);
				}
				else {
					for (int j = 0; j < endingSeeds[i - 1].Count; j++) {
						SetCurrentState(endingSeeds[i - 1][j]);
						startState = endingSeeds[i - 1][j];

						routes[i].Run(this, i);
					}
				}
			}
			for (int sec = 0; sec < sectionSOB.Length; sec++) {
				if (sectionSOB[sec] == int.MaxValue)
					sectionSOB[sec] = 0;
			}
			for (int sec = 0; sec < sectionSOB.Length; sec++) {

				int testCost = 0;
				for (int i = sec; i < sectionSOB.Length; i++) {
					testCost += sectionSOB[i];
				}
				sectionSOB[sec] = testCost;
			}
			for (int sec = sectionSOB.Length - 3; sec < sectionSOB.Length; sec++) {
				sectionSOB[sec] = 0;
			}

			AddSeed(0, 0, 0);

			RouteSection? getOptimal(RNGState seed, int section, int cost) {

				RouteSection? bestRoute = null;

				if (!routeLookup.ContainsKey(seed)) {
					return null;
				}

				foreach (var p in routeLookup[seed]) {
					var route = routeSections[p];

					if (route.Index == section) {

						if (section == routes.Length - 1) {
							int localCost = cost + route.Cost;

							if (SetNewBest(localCost)) {
								bestRoute = route;
							}
						}
						else {
							RouteSection? test = null;
							int testCost = cost + route.Cost + sectionSOB[section + 1];
							if (testCost < BestCost) {
								test = getOptimal(route.EndingSeed, section + 1, cost + route.Cost);
							}

							if (test != null) {

								route.nextPart = test;
								test.lastPart = route;

								bestRoute = route;

							}
						}
					}
				}

				return bestRoute;
			}

			return getOptimal(new RNGState(){ seed = rootSeed }, 0, 0);

		}
		public IEnumerable GetAllChests(RouteSection finalResult) {

			random.GetInteger();

			for (int i = 0; i < routes.Length; i++) {
				foreach (var chest in routes[i].GetChests(this, finalResult.AmountOfThing)) {
					yield return chest;
				}
				finalResult = finalResult.nextPart!;
			}
		}

	}
}
