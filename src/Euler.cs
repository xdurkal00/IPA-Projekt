namespace ODESolver;

// class for Euler method solving
static class EulerMethod{
	private static Stopwatch stopwatch = new();
	// function for calculating linear ODE f with a value of y at time 0 until time n with a time step of h
	public static void Euler(Func<double,double,double> f, double y, List<double> vals, out TimeSpan exec_time){
		stopwatch.Start();
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.Euler){
			vals.Add(y);
			y += Consts.IntegrationSteps.Euler * f(i, y);
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}
	public static void Euler(Func<double,double,double,double> y_prime, Func<double,double,double,double> z_prime, double y, double z, List<(double, double)> vals, out TimeSpan exec_time){
		stopwatch.Reset();
		stopwatch.Start();
		double new_y, new_z;
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.Euler){
			vals.Add((y,z));
			new_y = y + Consts.IntegrationSteps.Euler * y_prime(i, y, z);
			new_z = z + Consts.IntegrationSteps.Euler * z_prime(i, y, z);
			(y, z) = (new_y, new_z);
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}
	public static void Euler(Matrix<double> m, Matrix<double> initial, List<Matrix<double>> vals, out TimeSpan exec_time){
		stopwatch.Reset();
		stopwatch.Start();
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.Euler){
			vals.Add(initial);
			initial += Consts.IntegrationSteps.Euler*m*initial;
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}
}
