using Business.Interfaces;
using Business.Services;
using System;
using System.Threading;

public class ExpiredTokenService
{
    private Timer _timer;
    private readonly ISeguridadService _seguridadService;

    public ExpiredTokenService()
    {
        _seguridadService = new SeguridadService();
    }
    public void Start()
    {
        _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
    }

    public void Stop()
    {
        _timer?.Dispose();
    }

    private void ExecuteTask(object state)
    {
        _seguridadService.VerificarTokensVencidos();
    }
}
