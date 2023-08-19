#define Test
using System;
using System.Net.Http.Headers;
using System.Drawing.Imaging;
using WarioMoD.RNG;
using System.Reflection;

namespace WarioMoD {
	public partial class Form1 : Form {
		DateTime start = new DateTime();

		 System.Timers.Timer delay;

		int count = 0;


		int seedsComplete;

		public Form1() {
			InitializeComponent();
			isHumanRunning.Checked = (bool)Properties.Settings.Default["HumanRunning"];
			RouteFinder.HumanRunning = isHumanRunning.Checked;
			timerDelay.Value = (int)Properties.Settings.Default["TimerDelay"];
		}

		int threadsRunning;
		object optimalLock = new object(), seedLock = new object();
		int nextSeed;

		int minCost;
		RouteSection foundRoute;

		StraightShot[] route;

		Thread[] searchThreads;

		void Run() {



			while (true) {
				int next = GetNextSeed();
				if (next < 0)
					return;

				var r = new RouteFinder(next, route);

				CheckOptimal(r.Run(true));

				SetCompletedSeeds(seedsComplete + 1);
			}
			
		}

		int GetNextSeed() {
			lock (seedLock) {
				if (nextSeed >= 60) {
					threadsRunning--;
					if (threadsRunning == 0) {
						ExportResult();
							
						BeginInvoke(() => {
							EnableControls(true);
						});
					}
					return -1;
				}
				nextSeed++;

				return nextSeed - 1;
			}
		}
		void CheckOptimal(RouteSection? section) {
			if (section == null)
				return;

			int totalCost = 0;
			for (var s = section; s != null; s = s.nextPart) {
				totalCost += s.Cost;
			}

			lock (optimalLock) {
				if (totalCost < minCost) {
					minCost = totalCost;
					foundRoute = section!;
				}
			}
		}

		void EnableControls(bool enabled) {
			foreach (Control ctrl in Controls) {
				if (ctrl is Button || ctrl is ComboBox || ctrl is NumericUpDown)
					ctrl.Enabled = enabled;
			}
		}

		private void SetCompletedSeeds(int amount) {
			lock (seedLock) {
				seedsComplete = amount;

				BeginInvoke(() => {
					searchBar.Value = amount;
					searchLabel.Text = $"{amount}/60";
				});
			}
		}

		unsafe void ExportResult() {
			RouteFinder.Exporting = true;

			using (StreamWriter writer = new StreamWriter(File.Open("output.txt", FileMode.Create, FileAccess.Write))) {
				writer.WriteLine($"Starting seed: {(int)foundRoute.StartingSeed.seed}");
				for (var s = foundRoute; s.nextPart != null; s = s.nextPart) {
					writer.WriteLine($"{s.FriendlyName}: {s.AmountOfThing}");
				}
			}

			StreamWriter exData = new StreamWriter(File.Open("other_chests.txt", FileMode.Create, FileAccess.Write));

			var finder = new RouteFinder(foundRoute.StartingSeed.seed, route);

			foreach (var chest in finder.GetAllChests(foundRoute)) {
				if (chest is GreenChest) {
					var g = chest as GreenChest;

					int index = 0;

					switch (g.Type) {
						case GreenChestType.Arty:
							index = 2;
							break;
						case GreenChestType.Cosmic:
							index = 0;
							break;
						case GreenChestType.Captain:
							index = 8;
							break;
						case GreenChestType.Sparky:
							index = 10;
							break;
						case GreenChestType.Dragon:
							index = 4;
							break;
						case GreenChestType.Devil:
							index = 11;
							break;
						case GreenChestType.Genius:
							index = 6;
							break;
					}
					if (g.Upgrade)
						index++;

					using (Bitmap ind = new Bitmap($"sprites/indices{index:D2}.bmp")) {

						using (Bitmap colors = new Bitmap($"sprites/colors{index:D2}.png")) {

							using (Bitmap result = new Bitmap(colors.Width, colors.Height, PixelFormat.Format32bppArgb)) {

								var rLock = result.LockBits(new Rectangle(Point.Empty, result.Size), ImageLockMode.WriteOnly, result.PixelFormat);
								var cLock = colors.LockBits(new Rectangle(Point.Empty, colors.Size), ImageLockMode.ReadOnly, colors.PixelFormat);
								var iLock = ind.LockBits(new Rectangle(Point.Empty, ind.Size), ImageLockMode.ReadOnly, ind.PixelFormat);

								int* output = (int*)rLock.Scan0, inputC = (int*)cLock.Scan0;
								byte* inputI = (byte*)iLock.Scan0;

								for (int y = 0; y < colors.Height; y++) {
									for (int x = 0; x < colors.Width; x++) {

										*output = unchecked((int)0xFF086938);

										if (*inputI == 1) {
											*output = *inputC;
										}
										else if (*inputI <= 1) {
										}
										else {
											int val = *inputI - 2;

											if (g.FilledPieces.Contains(val))
												*output = *inputC;
										}

										output++;
										inputI++;
										inputC++;
									}
									output -= colors.Width;
									output += rLock.Stride >> 2;

									inputC -= colors.Width;
									inputC += cLock.Stride >> 2;

									inputI -= colors.Width;
									inputI += iLock.Stride;
								}

								//b.Save($"aa greenChest_{g.Type}{(g.Upgrade ? "_upgrade" : "")}.png");

								result.UnlockBits(rLock);
								result.Save($"greenChest_{g.Type}{(g.Upgrade ? "_upgrade" : "")}.png");
							}
						}
					}
				}
				else if (chest is RedPurpleChest) {
					var r = (chest as RedPurpleChest)!;

					exData.WriteLine($"{r.currentType}");
				}
			}

			exData.WriteLine();
			foreach (var a in RouteFinder.SphynxAnswers) {
				exData.WriteLine(a);
			}

			exData.Dispose();


			RouteFinder.Exporting = false;
		}

		private void FindRoute(object sender, EventArgs e) {

			var dist = WarioRandom.Distance("77ACE9A2D656A8FD", "013A1BDD3A9E2AC9", true, true);

			List<int> costs = new List<int>();
			List<List<RedChestType>> types = new List<List<RedChestType>>();
			for (int i = 18; i < 50; i++) {
				var rng = new WarioRandom("013A1BDD3A9E2AC9");
				var rout = new RouteFinder(0, new StraightShot[0]);
				rout.random = rng;


				for (int j = 0; j < 64 + (i + 1); j++)
					rng.GetInteger();

				if (rng.GetRange(4) == 0) {
					costs.Add(10000);
					continue;
				}
				int cost = 0;

				var chest = new RedPurpleChest(rout, true);
				cost += chest.Cost;
				chest = new RedPurpleChest(rout, true);
				cost += chest.Cost;

				for (int j = 0; j < 9; j++)
					rng.GetInteger();
				chest = new RedPurpleChest(rout, true);
				cost += chest.Cost;
				for (int j = 0; j < 18; j++)
					rng.GetInteger();
				chest = new RedPurpleChest(rout, true);
				cost += chest.Cost;

				costs.Add(cost + i);
			}
			//while (true) {

			//	for (int i = 0; i < 10000; i++) {
			//		var rng = new WarioRandom(i);

			//		var chest = new GreenChest(GreenChestType.Sparky, false, rng);

			//	}

			//}

			string file;
			using (var dialog = new OpenFileDialog()) {

				dialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				//dialog.DefaultExt = ".txt";
				dialog.Filter = "Text files (*.txt)|*.txt";
				var result = dialog.ShowDialog();

				if (result == DialogResult.Cancel) {
					return;
				}

				file = dialog.FileName;
			}

			minCost = int.MaxValue;
			nextSeed = 0;

			EnableControls(false);

			RouteFinder.BestCost = int.MaxValue;

			route = StraightShot.GetRoute(file);

			if (route.Length == 0) {
				EnableControls(true);
				return;
			}

			SetCompletedSeeds(0);
			threadsRunning = 4;
			searchThreads = new Thread[threadsRunning];

			for (int i = 0; i < threadsRunning; i++) {
				var thread = searchThreads[i] = new Thread(Run);
				thread.IsBackground = true;
				thread.Start();
			}
		}

		private void OnFormClosing(object sender, FormClosedEventArgs e) {

		}

		private void RequestTimerStart(object sender, EventArgs e) {
			if (delay == null) {
				runTimer.Text = "Stop Timer";
				start = DateTime.Now;

				double offset = (double)timerDelay.Value;
				offset -= 10.5;

				if (offset - 22 <= 0) {
					offset += 60;
				}


				delay = new System.Timers.Timer();
				delay.Interval = offset * 1000;
				delay.Elapsed += Delay_Elapsed;
				delay.Start();
			}
			else {
				runTimer.Text = "Start Timer";
				delay.Stop();
			}
		}

		private void isHumanRunning_Click(object sender, EventArgs e) {
			isHumanRunning.Checked = !isHumanRunning.Checked;

			Properties.Settings.Default["HumanRunning"] = isHumanRunning.Checked;
			RouteFinder.HumanRunning = isHumanRunning.Checked;
			Properties.Settings.Default.Save();
		}

		private void timerDelay_ValueChanged(object sender, EventArgs e) {

			timerDelay.Value = timerDelay.Value % 60;
			Properties.Settings.Default["TimerDelay"] = (int)timerDelay.Value;
			Properties.Settings.Default.Save();
		}

		private void Delay_Elapsed(object? sender, System.Timers.ElapsedEventArgs e) {
			delay.Stop();

			if (count == 6) {
				Console.Beep(1000, 400);

				count = 0;
				delay = null!;
			}
			else if (count <= 2) {
				Console.Beep(500, 150);

				delay.Interval = 1000;
				delay.Start();
				count++;
			}
			else {
				Console.Beep(500, 150);

				delay.Interval = 500;
				delay.Start();
				count++;
			}


		}
	}
}