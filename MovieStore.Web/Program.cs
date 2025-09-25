using Microsoft.EntityFrameworkCore;
using MovieStore.Data.Context;
using MovieStore.Data.Repositories;
using MovieStore.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();

// Add Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add repositories and services (3-layer architecture)
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IFilmeService, FilmeService>();
builder.Services.AddScoped<ILocacaoService, LocacaoService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ensure database is created and seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.EnsureCreatedAsync();
    
    // Seed data if empty
    if (!await context.Usuarios.AnyAsync())
    {
        await SeedDataAsync(context);
    }
}

app.Run();
static async Task SeedDataAsync(AppDbContext context)
{
    // Seed Users
    context.Usuarios.AddRange(
        new MovieStore.Data.Entities.Usuario
        {
            Nome = "Administrador",
            Email = "admin@moviestore.com",
            Login = "admin",
            Senha = "123456"
        },
        new MovieStore.Data.Entities.Usuario
        {
            Nome = "Funcionário",
            Email = "funcionario@moviestore.com",
            Login = "funcionario",
            Senha = "123456"
        }
    );

    // Seed Clients
    context.Clientes.AddRange(
        new MovieStore.Data.Entities.Cliente
        {
            Nome = "João Silva",
            CPF = "123.456.789-00",
            Email = "joao.silva@email.com",
            Telefone = "(11) 99999-1234",
            Endereco = "Rua das Flores, 123",
            DataNascimento = new DateTime(1985, 5, 15)
        },
        new MovieStore.Data.Entities.Cliente
        {
            Nome = "Maria Santos",
            CPF = "987.654.321-00",
            Email = "maria.santos@email.com",
            Telefone = "(11) 88888-5678",
            Endereco = "Av. Principal, 456",
            DataNascimento = new DateTime(1990, 8, 22)
        },
        new MovieStore.Data.Entities.Cliente
        {
            Nome = "Pedro Oliveira",
            CPF = "456.789.123-00",
            Email = "pedro.oliveira@email.com",
            Telefone = "(11) 77777-9012",
            Endereco = "Rua Central, 789",
            DataNascimento = new DateTime(1988, 12, 3)
        }
    );

    // Seed Movies
    context.Filmes.AddRange(
        new MovieStore.Data.Entities.Filme
        {
            Titulo = "O Poderoso Chefão",
            Descricao = "A saga de uma família mafiosa italiana nos Estados Unidos.",
            Genero = "Drama",
            Diretor = "Francis Ford Coppola",
            AnoLancamento = 1972,
            DuracaoMinutos = 175,
            ValorLocacao = 5.50m,
            QuantidadeDisponivel = 3
        },
        new MovieStore.Data.Entities.Filme
        {
            Titulo = "Matrix",
            Descricao = "Um hacker descobre a verdade sobre a realidade.",
            Genero = "Ficção Científica",
            Diretor = "Lana Wachowski",
            AnoLancamento = 1999,
            DuracaoMinutos = 136,
            ValorLocacao = 5.00m,
            QuantidadeDisponivel = 2
        },
        new MovieStore.Data.Entities.Filme
        {
            Titulo = "Pulp Fiction",
            Descricao = "Histórias interconectadas de crime em Los Angeles.",
            Genero = "Crime",
            Diretor = "Quentin Tarantino",
            AnoLancamento = 1994,
            DuracaoMinutos = 154,
            ValorLocacao = 4.50m,
            QuantidadeDisponivel = 4
        },
        new MovieStore.Data.Entities.Filme
        {
            Titulo = "Forrest Gump",
            Descricao = "A vida extraordinária de um homem simples.",
            Genero = "Drama",
            Diretor = "Robert Zemeckis",
            AnoLancamento = 1994,
            DuracaoMinutos = 142,
            ValorLocacao = 4.00m,
            QuantidadeDisponivel = 3
        },
        new MovieStore.Data.Entities.Filme
        {
            Titulo = "Inception",
            Descricao = "Um ladrão que invade sonhos para roubar segredos.",
            Genero = "Ficção Científica",
            Diretor = "Christopher Nolan",
            AnoLancamento = 2010,
            DuracaoMinutos = 148,
            ValorLocacao = 6.00m,
            QuantidadeDisponivel = 2
        },
        new MovieStore.Data.Entities.Filme
        {
            Titulo = "Titanic",
            Descricao = "Um romance épico no navio que afundou.",
            Genero = "Romance",
            Diretor = "James Cameron",
            AnoLancamento = 1997,
            DuracaoMinutos = 195,
            ValorLocacao = 5.00m,
            QuantidadeDisponivel = 5
        }
    );

    await context.SaveChangesAsync();

    // Seed some rentals
    var cliente1 = await context.Clientes.FirstAsync();
    var cliente2 = await context.Clientes.Skip(1).FirstAsync();
    var filme1 = await context.Filmes.FirstAsync();
    var filme2 = await context.Filmes.Skip(1).FirstAsync();

    context.Locacoes.AddRange(
        new MovieStore.Data.Entities.Locacao
        {
            ClienteId = cliente1.Id,
            FilmeId = filme1.Id,
            DataDevolucaoPrevista = DateTime.Now.AddDays(3),
            Valor = filme1.ValorLocacao
        },
        new MovieStore.Data.Entities.Locacao
        {
            ClienteId = cliente2.Id,
            FilmeId = filme2.Id,
            DataDevolucaoPrevista = DateTime.Now.AddDays(5),
            Valor = filme2.ValorLocacao
        }
    );

    await context.SaveChangesAsync();
}
