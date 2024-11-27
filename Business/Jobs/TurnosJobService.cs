using System;
using System.Data.SqlClient;
using System.Threading;

public class TurnosJobService
{
	private Timer _timer;
	private readonly _INTERVALO_JOB;

	public TurnosJobService()
	{
		_INTERVALO_JOB = intervaloJob;
	}

	public void Start()
	{
		_timer = new Timer(ExecuteTask, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
	}

	public void Stop()
	{
		_timer?.Dispose();
	}

	private void ExecuteTask(object state)
	{
		try
		{
			
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error ejecutando el job: {ex.Message}");
		}
	}
}
