using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ODESolver;

static class TaylorSeries{
	private static Stopwatch stopwatch = new();

	private static double RecTaylor(double y, double h, double lambda, uint step, uint max_steps){
		if(step < max_steps)
			return y+RecTaylor((h/(++step))*lambda*y, h, lambda, step, max_steps);
		return (h/(++step))*lambda*y;
	}

	public static void Taylor(Func<double,double> f, double y, double n, double h, double lambda, uint steps, List<double> vals, out TimeSpan exec_time){
		stopwatch.Start();
		for(double i = 0; i<=n; i+=h){
			vals.Add(y);
			y += RecTaylor(h*f(y), h, lambda, 1, steps);
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}
}
