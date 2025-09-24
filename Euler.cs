using System;
using System.Diagnostics;

namespace ODESolver;

// class for Euler method solving
static class EulerMethod{
	private static Stopwatch stopwatch = new();
	// function for calculating linear ODE f with a value of y at time 0 until time n with a time step of h
	public static void Euler(Func<double,double> f, double y, List<double> vals, out TimeSpan exec_time){
		stopwatch.Start();
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.Euler){
			vals.Add(y);
			y += Consts.IntegrationSteps.Euler * f(y);
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}
	public static void Euler(Func<double,double> y_prime, Func<double,double> z_prime, double y, double z, List<(double, double)> vals, out TimeSpan exec_time){
		stopwatch.Reset();
		stopwatch.Start();
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.Euler){
			vals.Add((y,z));
			y += Consts.IntegrationSteps.Euler * y_prime(z);
			z += Consts.IntegrationSteps.Euler * z_prime(y);
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}
}
