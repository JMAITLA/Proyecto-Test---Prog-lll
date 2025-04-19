using Microsoft.Playwright;
using NUnit.Framework;
using System;

[TestFixture]
public class RegistroIngresoTests
{
    private IPlaywright playwright;
    private IBrowser browser;
    private IPage page;

    [SetUp]
    public async Task Setup()
    {
        playwright = await Playwright.CreateAsync();
        browser = await playwright.Chromium.LaunchAsync();
        page = await browser.NewPageAsync();
        await page.GotoAsync(" http://localhost:5043"); 
    }

    [TearDown]
    public async Task Teardown()
    {
        await browser.CloseAsync();
        await playwright.DisposeAsync();
    }

    [Test]
    public async Task VerificarRegistroHoraIngreso()
    {
        // Seleccionar tipo de vehículo
        await page.SelectOptionAsync("#tipoVehiculo", "Automóvil");

        // Simular clic en el botón de ingreso
        await page.ClickAsync("#botonIngresar");

        // Obtener la hora de ingreso mostrada en el ticket (simulado)
        var horaIngresoTexto = await page.InnerTextAsync("#horaIngresoTicket");

        // Obtener la hora actual del sistema
        DateTime horaActual = DateTime.Now;

        // Convertir la hora del ticket a DateTime (manejar posibles formatos)
        DateTime horaIngresoTicket;
        DateTime.TryParse(horaIngresoTexto, out horaIngresoTicket);

        // Verificar que la hora de ingreso del ticket sea cercana a la hora actual
        Assert.IsTrue(Math.Abs((horaActual - horaIngresoTicket).TotalSeconds) < 5,
            $"La hora de ingreso ({horaIngresoTicket}) no coincide con la hora actual ({horaActual}).");
    }
}