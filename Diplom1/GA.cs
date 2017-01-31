﻿using System;
using System.Collections.Generic;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Domain;

namespace Diplom1
{
	public class GA
	{
		public static void Start()
		{
			var selection = new EliteSelection();
			var crossover = new UniformCrossover();
			var mutation = new UniformMutation();
			var fitness = new MyProblemFitness();
			var chromosome = new MyProblemChromosome();
			var population = new Population(50, 70, chromosome);

			var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
			ga.Termination = new GenerationNumberTermination(100);

			Console.WriteLine("GA running...");
			ga.Start();

			Console.WriteLine($"Best solution found has {ga.BestChromosome.Fitness} fitness.");
			int cnt = 0;
			using (var fw = new System.IO.StreamWriter("params.txt"))
			{
				foreach (var x in ga.BestChromosome.GetGenes())
				{
					if (cnt > 0 && cnt % 7 == 0)
						fw.WriteLine();
					cnt++;
					fw.Write($"{x}  ");
				}
			}
			Console.ReadKey();
		}
	}

	public class MyProblemChromosome : ChromosomeBase
	{
		public MyProblemChromosome() : base(20)
		{
			CreateGenes();
		}

		public override Gene GenerateGene(int geneIndex)
		{
			double value = 0;
			switch (geneIndex)
			{
				case 0:
				case 7:
					value = RandomizationProvider.Current.GetDouble(-10, -0.1);
					break;
				case 1:
				case 8:
					value = RandomizationProvider.Current.GetDouble(-0.1, -0.03);
					break;
				case 2:
				case 9:
					value = RandomizationProvider.Current.GetDouble(-0.03, 0);
					break;
				case 4:
				case 11:
					value = RandomizationProvider.Current.GetDouble(0, 0.03);
					break;
				case 5:
				case 12:
					value = RandomizationProvider.Current.GetDouble(0.03, 0.1);
					break;
				case 6:
				case 13:
					value = RandomizationProvider.Current.GetDouble(0.1, 10);
					break;
				case 14:
					value = RandomizationProvider.Current.GetDouble(0, 5.6);
					break;
				case 15:
					value = RandomizationProvider.Current.GetDouble(5.6, 11.1);
					break;
				case 16:
					value = RandomizationProvider.Current.GetDouble(11.1, 22.2);
					break;
				case 17:
					value = RandomizationProvider.Current.GetDouble(22.2, 33.3);
					break;
				case 18:
					value = RandomizationProvider.Current.GetDouble(33.3, 50);
					break;
				case 19:
					value = RandomizationProvider.Current.GetDouble(0, 1);
					break;
			}
			return new Gene(value);
		}

		public override IChromosome CreateNew()
		{
			return new MyProblemChromosome();
		}
	}

	public class MyProblemFitness : IFitness
	{
		public double Evaluate(IChromosome chromosome)
		{
			int ms = 2000;

			List<double> param = new List<double>();
			foreach (var x in chromosome.GetGenes())
			{
				param.Add((double)x.Value);
			}

			List<int> time = new List<int>();
			for (int i = 0; i < ms; ++i)
			{
				time.Add(i);
			}

			double perfectDist = 30;
			double curDist = 300;
			double mySpeed = 0;
			double entrySpeed = 16.7;

			var sol = new Solution(param[0], param[1], param[2], param[3], param[4],
				param[5], param[6], param[7], param[8], param[9], param[10],
				param[11], param[12], param[13], param[14], param[15], param[16],
				param[17], param[18], param[19], perfectDist, curDist, mySpeed, entrySpeed);
			var res = sol.ToSolve(ms);

			double eval = 0;
			foreach (var x in res.Distances)
			{
				if (x > 0 && Math.Abs(perfectDist - x) <= 10)
				{
					eval++;
				}
				else if (x < 0)
				{
					eval--;
				}
			}
			return eval;
		}
	}
}
