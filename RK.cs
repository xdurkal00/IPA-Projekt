using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ODESolver;

static class RK{
	private static Stopwatch stopwatch = new();

	private static (double, double) CalculateK2(Func<double,double> f, double y){
		double k_1 = Consts.IntegrationSteps.RK*f(y);
		double k_2 = Consts.IntegrationSteps.RK*f(y + Consts.IntegrationSteps.RK*(k_1/2));
		return (k_1, k_2);
	}

	public static void SecondOrd(Func<double,double> f, double y, List<double> vals, out TimeSpan exec_time){
		stopwatch.Start();
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.RK){
			vals.Add(y);
			(double k_1, double k_2) = CalculateK2(f, y);
			y += (k_1 + k_2)/2;
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}

	public static void FourthOrd(Func<double,double> f, double y, List<double> vals, out TimeSpan exec_time){
		stopwatch.Start();
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.RK){
			vals.Add(y);
			(double k_1, double k_2) = CalculateK2(f, y);
			double k_3 = Consts.IntegrationSteps.RK*f(y + Consts.IntegrationSteps.RK*(k_2/2));
			double k_4 = Consts.IntegrationSteps.RK*f(y + Consts.IntegrationSteps.RK*k_3);
			y += (k_1 + 2*k_2 + 2*k_3 + k_4)/6;
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}
}
