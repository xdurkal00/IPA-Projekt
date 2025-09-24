using System;
using System.Diagnostics;

namespace ODESolver;

// class for Euler method solving
static class EulerMethod{
	private static Stopwatch stopwatch = new();
	public static bool Convergence(double h, double lambda) => Math.Abs(1 + h*lambda) <= 1;
	// function for calculating linear ODE f with a value of y at time 0 until time n with a time step of h
	public static void Euler(Func<double,double> f, double y, double n, double h, List<double> vals, out TimeSpan exec_time){
		stopwatch.Start();
		for(double i = 0; i<=n; i+=h){
			vals.Add(y);
			y += h * f(y);
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}
}
