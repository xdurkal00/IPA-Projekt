using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ODESolver;

static class TaylorSeries{
	private static Stopwatch stopwatch = new();

	private static double RecTaylor(double y, uint step, double last_term){
		double new_term = ((Consts.IntegrationSteps.Taylor/++step)*Consts.Lambda*y);
		if(step < Consts.TaylorSeries.MaxSteps && Math.Abs(last_term)+Math.Abs(y)+Math.Abs(new_term) > Consts.TaylorSeries.EPS)
			return y+RecTaylor(new_term, step, y);
		return new_term;
	}

	public static void Taylor(Func<double,double> f, double y, List<double> vals, out TimeSpan exec_time){
		stopwatch.Start();
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.Taylor){
			vals.Add(y);
			y += RecTaylor(Consts.IntegrationSteps.Taylor*f(y), 1, y);
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}
}
