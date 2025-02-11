using Management.Service.Domain.Services.Interfaces;

namespace Management.Service.Api.BackgroundServices;

public class FakeGoodsHostedService : BackgroundService
{
    private const int FakeDataPerGen = 100;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<FakeGoodsHostedService> _logger;

    public FakeGoodsHostedService(IServiceProvider serviceProvider, ILogger<FakeGoodsHostedService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("{time} | FakeGoods service start executing", DateTime.Now);

        using PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromMinutes(60));
        try
        {
            do
            {
                await GenerateFakeData(FakeDataPerGen, cancellationToken);
            } while (await timer.WaitForNextTickAsync(cancellationToken));
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogInformation("{time} | FakeGoods service is stopped", DateTime.Now);
        }
    }

    private async Task GenerateFakeData(int amount, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{time} | FakeGoods service started generating fake goods.", DateTime.Now);

        await using var scope = _serviceProvider.CreateAsyncScope();

        IFurnitureGoodsService furnitureGoodsService =
            scope.ServiceProvider.GetRequiredService<IFurnitureGoodsService>();

        try
        {
            await furnitureGoodsService.MockFurnitureGoods(amount, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{time} | Unexpected exception occured during fake goods generation.", DateTime.Now);
        }

        _logger.LogInformation("{time} | FakeGoods service ended generating fake goods.", DateTime.Now);
    }
}