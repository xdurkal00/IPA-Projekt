namespace ODESolver;

public struct Consts{
	public struct InitialConditions{
		public const double Y = 0d;
		public const double Z = 1d;
	};
	public struct IntegrationSteps{
		public const double Euler = 0.5d;
		public const double RK = 0.1d;
		public const double Taylor = 0.1d;
	};
	public const double Lambda = -1d;
	public const double Omega = 1d;
	public const double T_max = 5d;
	public struct TaylorSeries{
		public const uint MaxSteps = 50;
		public const double EPS = 0.000005d; 
	};
};
