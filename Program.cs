using System;
using System.Collections.Generic;
using System.Linq;
using ScottPlot;
using System.Diagnostics;

namespace ODESolver;

class Solver{
	const double y_0 = 2d;
	const double k = -1d;
	const double h_e = 1d;
	const double h_rk = 1d;
	const double h_ts = 1d;
	const uint ts_steps = 5;
	const double n = 10;

	
	static List<double> result = new();
	static TimeSpan exec_time = new();
	
	public static double function(double y) => k*y;
	
	static double analytical(double t) => y_0 * Math.Pow(Math.E, k*t);

	public static void Main(){
		ScottPlot.Plot plot = new();

		TaylorSeries.Taylor(function, y_0, n, h_ts, k, ts_steps, result, out exec_time);

		Console.WriteLine($"Taylor method execution time: {exec_time.TotalMilliseconds} ms");

		plot.Add.Scatter(Enumerable.Range(0,result.Count+1).Select(i => i*h_e).ToArray(), result.ToArray()).LegendText = $"{ts_steps} term Taylor series";

		result.Clear();

		EulerMethod.Euler(function, y_0, n, h_e, result, out exec_time);

		Console.WriteLine($"Euler method execution time: {exec_time.TotalMilliseconds} ms");

		plot.Add.Scatter(Enumerable.Range(0,result.Count+1).Select(i => i*h_e).ToArray(), result.ToArray()).LegendText = "Euler method";
		Console.WriteLine($"Does Euler method converge: {EulerMethod.Convergence(h_e, k)}");				
		result.Clear();

		RK.SecondOrd(function, y_0, n, h_rk, result, out exec_time);
		
		Console.WriteLine($"2nd order Runge-Kutta method execution time: {exec_time.TotalMilliseconds} ms");

		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*h_rk).ToArray(), result.ToArray()).LegendText = "2nd Order Runge-Kutta";

		result.Clear();

		RK.FourthOrd(function, y_0, n, h_rk, result, out exec_time);

		Console.WriteLine($"4th order Runge-Kutta method execution time: {exec_time.TotalMilliseconds} ms");
	
		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*h_rk).ToArray(), result.ToArray()).LegendText = "4nd Order Runge-Kutta";
	
		


		var f = plot.Add.Function(analytical);
		f.MinX = 0;
		f.MaxX = n;
		f.LegendText = "Analytical solution";
		
		plot.Axes.SetLimits(0, n, Math.Min(y_0, result.Min()), Math.Max(y_0, result.Max()));
		plot.SavePng("graph.png", 600, 450);


	}
}

