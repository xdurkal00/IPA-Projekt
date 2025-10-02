namespace ODESolver;

static class TaylorSeries{
	private static Stopwatch stopwatch = new();

	private static double RecTaylor(double y, uint step, double last_term){
		double new_term = ((Consts.IntegrationSteps.Taylor/++step)*Consts.Lambda*y);
		if(step < Consts.TaylorSeries.MaxSteps && Math.Abs(last_term)+Math.Abs(y)+Math.Abs(new_term) > Consts.TaylorSeries.EPS)
			return y+RecTaylor(new_term, step, y);
		return new_term;
	}
	
	private static (double,double) RecTaylor(double y, double z, uint step, double last_term_y, double last_term_z){
		step++;
		double new_term_y = ((Consts.IntegrationSteps.Taylor/step)*Consts.Omega*z);
		double new_term_z = ((Consts.IntegrationSteps.Taylor/step)*-Consts.Omega*y);
		if(step < Consts.TaylorSeries.MaxSteps && (
					Math.Abs(last_term_y)+Math.Abs(y)+Math.Abs(new_term_y) > Consts.TaylorSeries.EPS ||
					Math.Abs(last_term_z)+Math.Abs(z)+Math.Abs(new_term_z) > Consts.TaylorSeries.EPS)){
			(double next_y, double next_z) = RecTaylor(new_term_y, new_term_z, step, y, z);
			return (y+next_y, z+next_z);
		}
		return (new_term_y, new_term_z);
	}

	private static Matrix<double> RecTaylor(Matrix<double> terms, Matrix<double> m, uint step, Matrix<double> last_terms){
		step++;
		Matrix<double> new_terms = ((Consts.IntegrationSteps.Taylor/step)*m*terms);
		if(step < Consts.TaylorSeries.MaxSteps && terms.FrobeniusNorm()+new_terms.FrobeniusNorm()+last_terms.FrobeniusNorm() > Consts.TaylorSeries.EPS){
			return terms+RecTaylor(new_terms, m, step, terms);
		}
		return new_terms;
	}

	public static void Taylor(Func<double,double,double> f, double y, List<double> vals, out TimeSpan exec_time){
		stopwatch.Reset();
		stopwatch.Start();
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.Taylor){
			vals.Add(y);
			y += RecTaylor(Consts.IntegrationSteps.Taylor*f(i, y), 1, y);
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}

	public static void Taylor(Func<double,double,double,double> y_prime, Func<double,double,double,double> z_prime, double y, double z, List<(double,double)> vals, out TimeSpan exec_time){
		stopwatch.Reset();
		stopwatch.Start();
		double exp_y, exp_z;
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.Taylor){
			vals.Add((y,z));
			(exp_y, exp_z) = RecTaylor(Consts.IntegrationSteps.Taylor*y_prime(i, y, z), Consts.IntegrationSteps.Taylor*z_prime(i, y, z), 1, y, z);
			y += exp_y;
			z += exp_z;
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}

	public static void Taylor(Matrix<double> m, Matrix<double> initial, List<Matrix<double>> vals, out TimeSpan exec_time){
		stopwatch.Reset();
		stopwatch.Start();
		for(double i = 0; i<=Consts.T_max; i+=Consts.IntegrationSteps.Taylor){
			vals.Add(initial);
			initial += RecTaylor(Consts.IntegrationSteps.Taylor*m*initial, m, 1, initial);
		}
		stopwatch.Stop();
		exec_time = stopwatch.Elapsed;
		stopwatch.Reset();
	}
}
