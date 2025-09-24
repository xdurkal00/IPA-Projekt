using System;
using System.Collections.Generic;
using System.Linq;
using ScottPlot;
using System.Diagnostics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace ODESolver;

class Solver{
	static List<double> result = new();
	static List<(double y, double z)> result2 = new();
	static TimeSpan exec_time = new();
	static ScottPlot.Plot plot2 = new();
	static List<Matrix<double>> result_m = new();
	
	public static void Main(){
		ScottPlot.Plot plot = new();

		Console.WriteLine($"Does function converge? {Functs.Convergence()}");

		//EulerMethod.Euler(Functs.Y_prime, Functs.Z_prime, Consts.InitialConditions.Y, Consts.InitialConditions.Z, result2, out exec_time);
		double[,] m = {{0,Consts.Omega},{-Consts.Omega,0}};
		double[,] initial = {{Consts.InitialConditions.Y},{Consts.InitialConditions.Z}};
		EulerMethod.EulerMatrix(DenseMatrix.OfArray(m), DenseMatrix.OfArray(initial), result_m, out exec_time);
		
		result_m.ForEach(i => Console.WriteLine(i[1,0]));

		plot2.Add.Scatter(Enumerable.Range(0, result_m.Count+1).Select(i => i*Consts.IntegrationSteps.Euler).ToArray(), result_m.Select(i => i[0,0]).ToArray()).LegendText = $"Euler method (y')";

		plot2.Add.Scatter(Enumerable.Range(0, result_m.Count+1).Select(i => i*Consts.IntegrationSteps.Euler).ToArray(), result_m.Select(i => i[1,0]).ToArray()).LegendText = $"Euler method (z')";

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
	
		


		var f = plot.Add.Function(Functs.Analytical_y);
		f.MinX = 0;
		f.MaxX = Consts.T_max;
		f.LegendText = "Analytical solution";

		var f_y = plot2.Add.Function(Functs.Analytical_y);
		f_y.MinX = 0;
		f_y.MaxX = Consts.T_max;
		f_y.LegendText = "Analytical solution for y'";

		var f_z = plot2.Add.Function(Functs.Analytical_z);
		f_z.MinX = 0;
		f_z.MaxX = Consts.T_max;
		f_z.LegendText = "Analytical solution for z'";

		plot2.Axes.SetLimits(0, Consts.T_max, -2, 2);
		plot2.SavePng("output/graph2.png", 600, 450);
		
		plot.Axes.SetLimits(0, Consts.T_max, 0, 2);
		plot.SavePng("output/graph.png", 600, 450);


	}
}

