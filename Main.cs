namespace ODESolver;

class Solver{
	static TimeSpan exec_time = new();

	static void SingleODETest(){
		ScottPlot.Plot plot = new();
		List<double> result = new();

		Console.WriteLine("Single equation test\n");

		TaylorSeries.Taylor(Functs.X_prime, Consts.InitialConditions.X, result, out exec_time);

		Console.WriteLine($"Taylor method execution time: {exec_time.TotalMilliseconds} s");

		plot.Add.Scatter(Enumerable.Range(0,result.Count+1).Select(i => i*Consts.IntegrationSteps.Taylor).ToArray(), result.ToArray()).LegendText = $"Taylor series (EPS={Consts.TaylorSeries.EPS})";

		result.Clear();

		EulerMethod.Euler(Functs.X_prime, Consts.InitialConditions.X, result, out exec_time);

		Console.WriteLine($"Euler method execution time: {exec_time.TotalMilliseconds} s");

		plot.Add.Scatter(Enumerable.Range(0,result.Count+1).Select(i => i*Consts.IntegrationSteps.Euler).ToArray(), result.ToArray()).LegendText = "Euler method";
		
		result.Clear();

		RK.SecondOrd(Functs.X_prime, Consts.InitialConditions.X, result, out exec_time);
		
		Console.WriteLine($"2nd order Runge-Kutta method execution time: {exec_time.TotalMilliseconds} s");

		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.RK).ToArray(), result.ToArray()).LegendText = "2nd order Runge-Kutta";

		result.Clear();

		RK.FourthOrd(Functs.X_prime, Consts.InitialConditions.X, result, out exec_time);

		Console.WriteLine($"4th order Runge-Kutta method execution time: {exec_time.TotalMilliseconds} s");
	
		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.RK).ToArray(), result.ToArray()).LegendText = "4nd order Runge-Kutta";

		var f = plot.Add.Function(Functs.Analytical.X);
		f.MinX = 0;
		f.MaxX = Consts.T_max;
		f.LegendText = "Analytical solution";

		plot.Axes.SetLimits(0, Consts.T_max, 0, 2);
		plot.SavePng("output/SingleEquation.png", 600, 450);
		
		Console.WriteLine("Output graph: output/SingeEquation.png\n");

	}

	static void TwoODETest(){
		ScottPlot.Plot plot = new();
		List<(double y,double z)> result = new();

		Console.WriteLine("2 equation system test\n");

		EulerMethod.Euler(Functs.Y_prime, Functs.Z_prime, Consts.InitialConditions.Y, Consts.InitialConditions.Z, result, out exec_time);

		Console.WriteLine($"Euler method execution time: {exec_time.TotalMilliseconds} s");

		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.Euler).ToArray(), result.Select(i => i.y).ToArray()).LegendText = "Euler method (y')";
		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.Euler).ToArray(), result.Select(i => i.z).ToArray()).LegendText = "Euler method (z')";

		result.Clear();

		RK.SecondOrd(Functs.Y_prime, Functs.Z_prime, Consts.InitialConditions.Y, Consts.InitialConditions.Z, result, out exec_time);

		Console.WriteLine($"2nd order Runge-Kutta method execution time: {exec_time.TotalMilliseconds} s");

		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.RK).ToArray(), result.Select(i => i.y).ToArray()).LegendText = "2nd order Runge-Kutta (y')";
		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.RK).ToArray(), result.Select(i => i.z).ToArray()).LegendText = "2nd order Runge-Kutta (z')";

		result.Clear();
	
		RK.FourthOrd(Functs.Y_prime, Functs.Z_prime, Consts.InitialConditions.Y, Consts.InitialConditions.Z, result, out exec_time);

		Console.WriteLine($"2nd order Runge-Kutta method execution time: {exec_time.TotalMilliseconds} s");

		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.RK).ToArray(), result.Select(i => i.y).ToArray()).LegendText = "4th order Runge-Kutta (y')";
		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.RK).ToArray(), result.Select(i => i.z).ToArray()).LegendText = "4th order Runge-Kutta (z')";

		result.Clear();

		TaylorSeries.Taylor(Functs.Y_prime, Functs.Z_prime, Consts.InitialConditions.Y, Consts.InitialConditions.Z, result, out exec_time);

		Console.WriteLine($"Taylor series execution time: {exec_time.TotalMilliseconds} s");
		
		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.Taylor).ToArray(), result.Select(i => i.y).ToArray()).LegendText = $"Taylor series (y') (EPS={Consts.TaylorSeries.EPS})";
		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.Taylor).ToArray(), result.Select(i => i.z).ToArray()).LegendText = $"Taylor series (z') (EPS={Consts.TaylorSeries.EPS})";



		result.Clear();	

		var f = plot.Add.Function(Functs.Analytical.Y);
		f.MinX = 0;
		f.MaxX = Consts.T_max;
		f.LegendText = "Analytical solution (y')";

		f = plot.Add.Function(Functs.Analytical.Z);
		f.MinX = 0;
		f.MaxX = Consts.T_max;
		f.LegendText = "Analytical solution (z')";

		plot.Axes.SetLimits(0, Consts.T_max, -5, 5);
		plot.SavePng("output/TwoEquations.png", 600, 450);

		Console.WriteLine("Output graph: output/TwoEquations.png\n");
	}

	static void MatrixTest(){
		ScottPlot.Plot plot = new();
		List<Matrix<double>> result = new();
		double[,] m = {{0,Consts.Omega},{-Consts.Omega,0}};
		double[,] initial = {{Consts.InitialConditions.Y},{Consts.InitialConditions.Z}};

		Console.WriteLine("2 equation system matrix test\n");

		EulerMethod.Euler(DenseMatrix.OfArray(m), DenseMatrix.OfArray(initial), result, out exec_time);

		Console.WriteLine($"Euler method execution time: {exec_time.TotalMilliseconds} s");

		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.Euler).ToArray(), result.Select(i => i[0,0]).ToArray()).LegendText = "Euler method (y')";
		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.Euler).ToArray(), result.Select(i => i[1,0]).ToArray()).LegendText = "Euler method (z')";

		result.Clear();

		RK.SecondOrd(DenseMatrix.OfArray(m), DenseMatrix.OfArray(initial), result, out exec_time);

		Console.WriteLine($"2nd order Runge-Kutta execution time: {exec_time.TotalMilliseconds} s");

		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.RK).ToArray(), result.Select(i => i[0,0]).ToArray()).LegendText = "2nd order Runge-Kutta (y')";
		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.RK).ToArray(), result.Select(i => i[1,0]).ToArray()).LegendText = "2nd order Runge-Kutta (z')";

		result.Clear();

		RK.FourthOrd(DenseMatrix.OfArray(m), DenseMatrix.OfArray(initial), result, out exec_time);

		Console.WriteLine($"4th order Runge-Kutta execution time: {exec_time.TotalMilliseconds} s");

		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.RK).ToArray(), result.Select(i => i[0,0]).ToArray()).LegendText = "4th order Runge-Kutta (y')";
		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.RK).ToArray(), result.Select(i => i[1,0]).ToArray()).LegendText = "4th order Runge-Kutta (z')";

		result.Clear();
		
		TaylorSeries.Taylor(DenseMatrix.OfArray(m), DenseMatrix.OfArray(initial), result, out exec_time);

		Console.WriteLine($"Taylor series execution time: {exec_time.TotalMilliseconds} s");

		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.Taylor).ToArray(), result.Select(i => i[0,0]).ToArray()).LegendText = $"Taylor series (y') (EPS={Consts.TaylorSeries.EPS})";
		plot.Add.Scatter(Enumerable.Range(0, result.Count+1).Select(i => i*Consts.IntegrationSteps.Taylor).ToArray(), result.Select(i => i[1,0]).ToArray()).LegendText = $"Taylor series (z') (EPS={Consts.TaylorSeries.EPS})";

		result.Clear();


		var f = plot.Add.Function(Functs.Analytical.Y);
		f.MinX = 0;
		f.MaxX = Consts.T_max;
		f.LegendText = "Analytical solution (y')";

		f = plot.Add.Function(Functs.Analytical.Z);
		f.MinX = 0;
		f.MaxX = Consts.T_max;
		f.LegendText = "Analytical solution (z')";

		plot.Axes.SetLimits(0, Consts.T_max, -5, 5);
		plot.SavePng("output/Matrix.png", 600, 450);

		Console.WriteLine("Output graph: output/Matrix.png\n");
	}

	public static void Main(){
		SingleODETest();
		TwoODETest();
		MatrixTest();
	}
}

