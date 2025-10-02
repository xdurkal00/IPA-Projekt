namespace ODESolver;

public struct Consts{
	public struct InitialConditions{
		public const double X = 2d;
		public const double Y = 0d;
		public const double Z = 2d;
	};
	public struct IntegrationSteps{
		public const double Euler = 0.1d;
		public const double RK = 0.1d;
		public const double Taylor = 0.1d;
	};
	public const double Lambda = -1d;
	public const double Omega = 1.5d;
	public const double T_max = 20d;
	public struct TaylorSeries{
		public const uint MaxSteps = 50;
		public const double EPS = 0.01d; 
	};
};
