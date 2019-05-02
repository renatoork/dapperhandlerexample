# Example Dapper Handler #

Esta package implementa algumas classes para permitir que o Dapper realize a tradução de `Guid`, `enum` para `char` e `string` automaticamente.

Em resumo, esta package permite que um *handler* seja registrado em *runtime* no Dapper para cada tipo listado acima informando-o sobre como deve ser feito a tradução.

### Como utilizar  ###

* Primeiro passo é instalar a package pelo NuGet
O repositório de packages da Example deve estar configurado no Visual Studio. Caso ainda não esteja configurado ver este [documento](https://megasistemas.atlassian.net/wiki/pages/viewpage.action?pageId=166724213 "link") no Confluence.
Abrir o *Manage Nuget Packages*  no Visual Studio e procurar pela package *Example.Dapper.Handler* e instalar. 
Caso não apareça para instalar, certifique-se que o repositorio da Example do MyGet esteja configurado corretamente ou se o nome que esta procurando esta correto. Se ainda não aparacer acesse a Arquitetura.

* Adicionar o *handler*  do enum
Para cada `enum` é necessário avisar o Dapper que deve manusear a tradução desta para `char`ou `string`. Isto é feito executando o método específico do tipo o `enum`.

* Decorar o enum conforme seu tipo
Quando o tipo for `char` basta  igualar o nome com seu respectivo valor. Quando for `string` é preciso colocar um *Annotation*  com o valor do nome. Para `bool` não é preciso decorador, basta definir o atributo com este tipo.

Veja a seguir exemplos de uso cada tipo.



### GUID ###
------------
Para atributos da model do tipo Guid não é necessário decorar o atributo, basta defini-lo como este tipo no *model*. 

*O handler do Guid é adicionado automaticamente no servidor através do pacote Example.AspNetCore.Extensions.*

##### Problemas  #####
1. Caso o handle não seja adicionado o Dapper envia para o banco ocorrerá um erro do tipo *ArgumentException*.

##### Exemplo #####
```csharp
namespace exemplo;
public class Pessoa
{
	public Guid Id {get; set;}
	public string Nome {get; set;}
	public bool Ativo {get; set;}
}

public class PessoaServico
{
    ...
	public void insert()
	{
	    Pessoa pessoa = new Pessoa();
		pessoa.Id = Guid.NewGuid();
	}
	...
}

using Example.Dapper.Handlers;
namespace exemplo;
public class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		...
	  	SqlHandler.AddGuid();
	}
}
```



### BOOL ###
------------
Para atributos da model do tipo bool não é necessário decorar o atributo, basta defini-lo como este tipo no *model*. 

*O handler do bool é adicionado automaticamente no servidor através do pacote Example.AspNetCore.Extensions.*

##### Problemas  #####
1. Caso o handle não seja adicionado o Dapper envia para o banco o caracter 0 para false ou 1 para true.

##### Exemplo #####
```csharp
namespace exemplo;
public class Pessoa
{
	public int Id {get; set;}
	public string Nome {get; set;}
	public bool Ativo {get; set;}
}

using Example.Dapper.Handlers;
namespace exemplo;
public class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		...
	  	SqlHandler.AddBool();
	}
}
```

### CHAR ###
------------
Para o enum do tipo char é necessário mapear os valores na declaração do enum igualando o nome com o seu valor e adicionar um handle para cada enum na classe serviço.

Para adicionar o handler para o tipo char é necessário usar o método correto deste tipo, *AddEnumChar*.

*É necessário adicionar o handler para cada enum e apenas uma vez. Com o handler adicionado, o Dapper faz a tradução de todos os atributos do tipo do enum.*

##### Problemas  #####
1. Se o handle do enum tipo char não for adicionado, o Dapper envia para o banco de dados o caracter ASCII correspondente ao valor do enum.
2. Se um valor não for mapeado para o enum, o Dapper envia para o banco de dados o valor int correspontende a numeração do enum, 0 para o primeiro, 1 para o segundo etc.
3. Será lançado a exceção *EnumCharNotMappedException*  ao tentar adicionar um handle de enum não mapeado. 

##### Exemplo #####
```csharp
namespace exemplo;
public enum Sexo
{
	Feminino = 'F',
	Masculino = 'M'
}

namespace exemplo;
public class Pessoa
{
	public int Id {get; set;}
	public string Nome {get; set;}
	public Sexo Sexo {get; set;}
}

using Example.Dapper.Handlers;
namespace exemplo;
public class Servico
{
	public Servico
	{
	  	SqlHandler.AddEnumChar<Sexo>();
	}
}
```

### STRING ###
------------
Para o enum do tipo string é necessário mapear os valores na declaração do enum colocando o atributo *EnumValue* em cada linha do enum e adicionar um handle para cada enum na classe serviço.

Para adicionar o handler para o tipo char é necessário usar o método correto deste tipo, *AddEnumString*.

*É necessário adicionar o handler para cada enum e apenas uma vez. Com o handler adicionado, o Dapper faz a tradução de todos os atributos do tipo do enum.*

##### Problemas  #####
1. Se o handle do enum do tipo string não for adicionado, o Dapper lança uma exceção do tipo *EnumStringParseException*.
2. Se um valor não for mapeado para o enum, o Dapper lança uma exceção do tipo *EnumStringParseException*  ao tentar adicionar o handle.
3. Será lançado a exceção *EnumStringNotMappedException*  ao tentar adicionar um handle de enum não definido/mapeado. 

##### Exemplo #####
```csharp
namespace exemplo;
public enum Situacao
{
	[EnumValue("AT")]
	Ativo,
	 [EnumValue("IN")]
	Inativo,
	 [EnumValue("BQ")]
	Bloqueado
}

namespace exemplo;
public class Pessoa
{
	public int Id {get; set;}
	public string Nome {get; set;}
	public Situacao Situacao {get; set;}
}

using Example.Dapper.Handlers;
namespace exemplo;
public class Servico
{
	public Servico
	{
	  	SqlHandler.AddEnumString<Situacao>();
	}
}
```


### NULLABLE ###
------------
Não deve ser definido atributos tipo *Nullable enum*, pois o Dapper não consegue tratar na tradução do null para o banco de dados e recuperar um null para o atributo da classe.

Para os valores nullable deve ser utilizado um item no enum para representar a opção null. 
- Para os enum do tipo `char`, este item deve ser obrigatoriamente mapeado com o valor 0 (zero) para que o Dapper o entenda no handle de tradução.
- Para os enum do tipo `string`, este item deve ser obrigatoriamente mapeado com o valor "" (vazio) para que o Dapper o entenda no handle de tradução.


##### Exemplo #####
- Exemplo de declaração nullable que não podemos utilizar
```csharp
namespace exemplo;
public class Pessoa
{
	public int Id {get; set;}
	public string Nome {get; set;}
	public Sexo? Sexo {get; set;} // exemplo de declaração null que não pode ser utilizado
	public Nullable<Sexo> Sexo {get; set;} // exemplo de declaração null que não pode ser utilizado
}
```

- Exemplo de uso para enum com possibilidade de valores null

```csharp
namespace exemplo;
public enum Sexo
{
	Nenhum = 0,
	Feminino = 'F',
	Masculino = 'M'
}

namespace exemplo;
public enum Situacao
{
	[EnumValue("")]
	Nenhum,
	[EnumValue("AT")]
	Ativo,
	 [EnumValue("IN")]
	Inativo,
	 [EnumValue("BQ")]
	Bloqueado
}

namespace exemplo;
public class Pessoa
{
	public int Id {get; set;}
	public string Nome {get; set;}
	public Sexo Sexo {get; set;}
	public Situacao Situacao {get; set;}
}

using Example.Dapper.Handlers;
namespace exemplo;
public class Servico
{
	public Servico
	{
	  	SqlHandler.AddEnumChar<Sexo>();
		SqlHandler.AddEnumString<Situacao>();
	}
}
```

### Dependências de packages###
- Example.Dapper