using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ODESolver;

static class RK{
	private static Stopwatch stopwatch = new();

	private static (double, double) CalculateK2(Func<double,double> f, double y, double h){
		double k_1 = h*f(y);
		double k_2 = h*f(y + h*(k_1/2));
		return (k_1, k_2);
	}

	public static void SecondOrd(Func<double,double> f, double y, double n, double h, List<double> vals, out TimeSpan exec_time){
		stopwatch.Start();
		for(double i = 0; i<=n; i+=h){
			vals.Add(y);
			(double k_1, double k_2) = CalculateK2(f, y, h);
			y += (k_1 + k_2)/2;
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}

	public static void FourthOrd(Func<double,double> f, double y, double n, double h, List<double> vals, out TimeSpan exec_time){
		stopwatch.Start();
		for(double i = 0; i<=n; i+=h){
			vals.Add(y);
			(double k_1, double k_2) = CalculateK2(f, y, h);
			double k_3 = h*f(y + h*(k_2/2));
			double k_4 = h*f(y + h*k_3);
			y += (k_1 + 2*k_2 + 2*k_3 + k_4)/6;
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}
}
