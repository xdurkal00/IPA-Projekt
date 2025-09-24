using System;
using System.Collections.Generic;
using System.Linq;
using ScottPlot;
using System.Diagnostics;

namespace ODESolver;

class Solver{
	static List<double> result = new();
	static TimeSpan exec_time = new();
	
	public static void Main(){
		ScottPlot.Plot plot = new();

		Console.WriteLine($"Does function converge? {Functs.Convergence()}");

		TaylorSeries.Taylor(Functs.Y_prime, Consts.InitialConditions.Y, result, out exec_time);

		Console.WriteLine($"Taylor method execution time: {exec_time.TotalMilliseconds} ms");

		plot.Add.Scatter(Enumerable.Range(0,result.Count+1).Select(i => i*Consts.IntegrationSteps.Euler).ToArray(), result.ToArray()).LegendText = $"Taylor series (EPS={Consts.TaylorSeries.EPS})";

		result.Clear();

		EulerMethod.Euler(Functs.Y_prime, Consts.InitialConditions.Y, result, out exec_time);

		Console.WriteLine($"Euler method execution time: {exec_time.TotalMilliseconds} ms");

		plot.Add.Scatter(Enumerable.Range(0,result.Count+1).Select(i => i*Consts.IntegrationSteps.Euler).ToArray(), result.ToArray()).LegendText = "Euler method";
		
		result.Clear();

		RK.SecondOrd(Functs.Y_prime, Consts.InitialConditions.Y, result, out exec_time);
		
		Console.WriteLine($"2nd order Runge-Kutta method execution time: {exec_time.TotalMilliseconds} ms");

		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.RK).ToArray(), result.ToArray()).LegendText = "2nd Order Runge-Kutta";

		result.Clear();

		RK.FourthOrd(Functs.Y_prime, Consts.InitialConditions.Y, result, out exec_time);

		Console.WriteLine($"4th order Runge-Kutta method execution time: {exec_time.TotalMilliseconds} ms");
	
		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.RK).ToArray(), result.ToArray()).LegendText = "4nd Order Runge-Kutta";
	
		


		var f = plot.Add.Function(Functs.Analytical);
		f.MinX = 0;
		f.MaxX = Consts.T_max;
		f.LegendText = "Analytical solution";
		
		plot.Axes.SetLimits(0, Consts.T_max, 0, 2);
		plot.SavePng("graph.png", 600, 450);


	}
}

