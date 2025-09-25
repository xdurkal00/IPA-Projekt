namespace ODESolver;

static class RK{
	private static Stopwatch stopwatch = new();

	private static (double, double) CalculateK2(Func<double,double> f, double y){
		double k_1 = Consts.IntegrationSteps.RK*f(y);
		double k_2 = Consts.IntegrationSteps.RK*f(y + Consts.IntegrationSteps.RK/2 * k_1);
		return (k_1, k_2);
	}

	private static (double,double,double,double) CalculateK2(Func<double,double> f_y, Func<double,double> f_z, double y, double z){
		double k_1y = Consts.IntegrationSteps.RK*f_y(z);
		double k_1z = Consts.IntegrationSteps.RK*f_z(y);
		double k_2y = Consts.IntegrationSteps.RK*f_y(z + Consts.IntegrationSteps.RK/2 * k_1z);
		double k_2z = Consts.IntegrationSteps.RK*f_z(y + Consts.IntegrationSteps.RK/2 * k_1y);
		return (k_1y, k_1z, k_2y, k_2z);
	}



	private static (Matrix<double>, Matrix<double>) CalculateK2(Matrix<double> m, Matrix<double> initial){
		Matrix<double> k_1 = Consts.IntegrationSteps.RK * m * initial;
		Matrix<double> k_2 = Consts.IntegrationSteps.RK * m * (initial + Consts.IntegrationSteps.RK/2 * k_1);
		return (k_1, k_2);
	}

	public static void SecondOrd(Func<double,double> f, double y, List<double> vals, out TimeSpan exec_time){
		stopwatch.Reset();
		stopwatch.Start();
		double k_1,k_2;
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.RK){
			vals.Add(y);
			(k_1, k_2) = CalculateK2(f, y);
			y += (k_1 + k_2)/2;
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}

	public static void SecondOrd(Func<double,double> y_prime, Func<double,double> z_prime, double y, double z, List<(double,double)> vals, out TimeSpan exec_time){
		stopwatch.Reset();
		stopwatch.Start();
		double k_1y, k_1z, k_2y, k_2z;
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.RK){
			vals.Add((y, z));
			(k_1y, k_1z, k_2y, k_2z) = CalculateK2(y_prime, z_prime, y, z);
			y += (k_1y + k_2y)/2;
			z += (k_1z + k_2z)/2;
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}

	public static void SecondOrd(Matrix<double> m, Matrix<double> initial, List<Matrix<double>> vals, out TimeSpan exec_time){
		stopwatch.Reset();
		stopwatch.Start();
		Matrix<double> k_1, k_2;
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.RK){
			vals.Add(initial);
			(k_1, k_2) = CalculateK2(m, initial);
			initial += (k_1 + k_2)/2;
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}

	public static void FourthOrd(Func<double,double> f, double y, List<double> vals, out TimeSpan exec_time){
		stopwatch.Reset();
		stopwatch.Start();
		double k_1, k_2, k_3, k_4;
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.RK){
			vals.Add(y);
			(k_1, k_2) = CalculateK2(f, y);
			k_3 = Consts.IntegrationSteps.RK*f(y + Consts.IntegrationSteps.RK/2 * k_2);
			k_4 = Consts.IntegrationSteps.RK*f(y + Consts.IntegrationSteps.RK*k_3);
			y += (k_1 + 2*k_2 + 2*k_3 + k_4)/6;
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}

	public static void FourthOrd(Func<double,double> y_prime, Func<double,double> z_prime, double y, double z, List<(double,double)> vals, out TimeSpan exec_time){
		stopwatch.Reset();
		stopwatch.Start();
		double k_1y, k_1z, k_2y, k_2z, k_3y, k_3z, k_4y, k_4z;
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.RK){
			vals.Add((y,z));
			(k_1y, k_1z, k_2y, k_2z) = CalculateK2(y_prime, z_prime, y, z);
			k_3y = Consts.IntegrationSteps.RK * y_prime(z + Consts.IntegrationSteps.RK/2 * k_2z);
			k_3z = Consts.IntegrationSteps.RK * z_prime(y + Consts.IntegrationSteps.RK/2 * k_2y);
			k_4y = Consts.IntegrationSteps.RK * y_prime(z + Consts.IntegrationSteps.RK * k_3z);
			k_4z = Consts.IntegrationSteps.RK * z_prime(y + Consts.IntegrationSteps.RK * k_3y);
			y += (k_1y + 2*k_2y + 2*k_3y + k_4y)/6;
			z += (k_1z + 2*k_2z + 2*k_3z + k_4z)/6;
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}

	public static void FourthOrd(Matrix<double> m, Matrix<double> initial, List<Matrix<double>> vals, out TimeSpan exec_time){
		stopwatch.Reset();
		stopwatch.Start();
		Matrix<double> k_1, k_2, k_3, k_4;
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.RK){
			vals.Add(initial);
			(k_1, k_2) = CalculateK2(m, initial);
			k_3 = Consts.IntegrationSteps.RK * m * (initial + Consts.IntegrationSteps.RK/2 * k_2);
			k_4 = Consts.IntegrationSteps.RK * m * (initial + Consts.IntegrationSteps.RK * k_3);
			initial += (k_1 + 2*k_2 + 2*k_3 + k_4)/6;
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}
}
