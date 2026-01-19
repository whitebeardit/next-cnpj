# next-CNPJ

Biblioteca .NET para validação robusta de CNPJ com suporte ao novo formato alfanumérico conforme NTC 2025.001.

## 📋 Sobre

A biblioteca **next-CNPJ** oferece validação completa de CNPJ (Cadastro Nacional da Pessoa Jurídica), incluindo suporte ao novo formato alfanumérico que entrará em vigor. Implementa todas as regras da NTC 2025.001, incluindo cálculo e validação de dígitos verificadores, normalização automática e identificação de formato.

## ✨ Características

- ✅ Validação de CNPJ numérico (formato tradicional)
- ✅ Validação de CNPJ alfanumérico (novo formato NTC 2025.001)
- ✅ Cálculo e validação automática de dígitos verificadores
- ✅ Normalização automática (remove formatação: pontos, barras, hífens)
- ✅ Identificação automática de formato (numérico ou alfanumérico)
- ✅ Configuração de letras excluídas (I, O, U, Q, F conforme ENCAT)
- ✅ Suporte a CNPJ com ou sem formatação
- ✅ Conversão automática de letras minúsculas para maiúsculas
- ✅ Resultados detalhados com mensagens de erro descritivas

## 🚀 Instalação

Instale o pacote via NuGet:

```bash
dotnet add package next-CNPJ
```

Ou via Package Manager Console:

```powershell
Install-Package next-CNPJ
```

## 📖 Uso Básico

### Validação Simples

```csharp
using next_CNPJ.Core.Services;

var validator = new CnpjValidator();

// Validação simples - retorna true/false
bool isValid = validator.IsValid("11222333000181");
Console.WriteLine($"CNPJ válido: {isValid}"); // true
```

### Validação Detalhada

```csharp
using next_CNPJ.Core.Services;
using next_CNPJ.Core.Domain;

var validator = new CnpjValidator();

// Validação detalhada - retorna informações completas
var result = validator.Validate("11222333000181");

if (result.IsValid)
{
    Console.WriteLine($"CNPJ válido!");
    Console.WriteLine($"Formato: {result.Format}"); // Numeric
    Console.WriteLine($"CNPJ normalizado: {result.NormalizedCnpj}"); // 11222333000181
}
else
{
    Console.WriteLine($"Erro: {result.ErrorMessage}");
}
```

### Validação com Formatação

A biblioteca aceita CNPJ com ou sem formatação:

```csharp
var validator = new CnpjValidator();

// Todas essas formas são aceitas:
validator.IsValid("11222333000181");           // Sem formatação
validator.IsValid("11.222.333/0001-81");      // Com formatação tradicional
validator.IsValid("12.ABC.345/01DE-35");      // Formato alfanumérico com formatação
```

## 🔤 CNPJ Alfanumérico

### Validação de CNPJ Alfanumérico

O novo formato alfanumérico permite letras na raiz (posições 1-8) ou na ordem (posições 9-12):

```csharp
var validator = new CnpjValidator();

// Exemplo de CNPJ alfanumérico válido
var result = validator.Validate("12ABC34501DE35");

if (result.IsValid)
{
    Console.WriteLine($"Formato: {result.Format}"); // Alphanumeric
    Console.WriteLine($"CNPJ normalizado: {result.NormalizedCnpj}"); // 12ABC34501DE35
}
```

### Letras Excluídas

Por padrão, as letras I, O, U, Q, F são excluídas conforme especificação técnica do ENCAT:

```csharp
var validator = new CnpjValidator();

// CNPJ com letra excluída (I) - inválido
var result = validator.Validate("12IBC34501DE35");
Console.WriteLine(result.IsValid); // false
Console.WriteLine(result.ErrorMessage); // "A letra 'I' não é permitida..."
```

### Configuração Customizada

Você pode customizar as letras excluídas ou permitir todas as letras:

```csharp
using next_CNPJ.Core.Domain;

var config = new CnpjConfiguration
{
    ExcludedLetters = new[] { 'I', 'O' }, // Apenas I e O excluídas
    AllowExcludedLetters = false
};

var validator = new CnpjValidator();
var result = validator.Validate("12IBC34501DE35", config);
```

Para permitir todas as letras (incluindo as normalmente excluídas):

```csharp
var config = new CnpjConfiguration
{
    AllowExcludedLetters = true // Permite todas as letras
};

var result = validator.Validate("12IBC34501DE35", config);
```

## 🔍 Identificação de Formato

Você pode identificar o formato do CNPJ antes de validar:

```csharp
using next_CNPJ.Core.Services;

var identifier = new CnpjFormatIdentifier();

// Identificar formato
var format = identifier.IdentifyFormat("12ABC34501DE35");
Console.WriteLine(format); // Alphanumeric

// Verificações rápidas
bool isAlphanumeric = identifier.IsAlphanumeric("12ABC34501DE35"); // true
bool isNumeric = identifier.IsNumeric("11222333000181"); // true
```

## 📚 API Reference

### `ICnpjValidator`

Interface principal para validação de CNPJ.

#### Métodos

- `bool IsValid(string? cnpj, CnpjConfiguration? config = null)`
  - Valida um CNPJ e retorna `true` se válido, `false` caso contrário.

- `CnpjValidationResult Validate(string? cnpj, CnpjConfiguration? config = null)`
  - Valida um CNPJ e retorna um objeto `CnpjValidationResult` com informações detalhadas.

### `CnpjValidationResult`

Resultado da validação com as seguintes propriedades:

- `bool IsValid` - Indica se o CNPJ é válido
- `CnpjFormat Format` - Formato identificado (Numeric ou Alphanumeric)
- `string? ErrorMessage` - Mensagem de erro (null se válido)
- `string NormalizedCnpj` - CNPJ normalizado (sem formatação)

### `ICnpjFormatIdentifier`

Interface para identificação do formato do CNPJ.

#### Métodos

- `CnpjFormat IdentifyFormat(string? cnpj)` - Identifica o formato do CNPJ
- `bool IsAlphanumeric(string? cnpj)` - Verifica se é alfanumérico
- `bool IsNumeric(string? cnpj)` - Verifica se é numérico

### `CnpjConfiguration`

Configuração para validação customizada.

#### Propriedades

- `char[] ExcludedLetters` - Letras que não devem ser aceitas (padrão: I, O, U, Q, F)
- `bool AllowExcludedLetters` - Permite letras excluídas mesmo que estejam na lista (padrão: false)

## 💡 Casos de Uso

### 1. Validação em Formulários Web

```csharp
public class CnpjValidationService
{
    private readonly ICnpjValidator _validator;

    public CnpjValidationService(ICnpjValidator validator)
    {
        _validator = validator;
    }

    public ValidationResult ValidateUserInput(string cnpj)
    {
        var result = _validator.Validate(cnpj);
        
        if (!result.IsValid)
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = result.ErrorMessage
            };
        }

        return new ValidationResult
        {
            IsValid = true,
            NormalizedCnpj = result.NormalizedCnpj,
            Format = result.Format.ToString()
        };
    }
}
```

### 2. Processamento em Lote

```csharp
public void ValidateBatch(IEnumerable<string> cnpjList)
{
    var validator = new CnpjValidator();
    var results = new List<CnpjValidationResult>();

    foreach (var cnpj in cnpjList)
    {
        var result = validator.Validate(cnpj);
        results.Add(result);
        
        if (result.IsValid)
        {
            Console.WriteLine($"✓ {cnpj} - {result.Format}");
        }
        else
        {
            Console.WriteLine($"✗ {cnpj} - {result.ErrorMessage}");
        }
    }
}
```

### 3. Integração com APIs

```csharp
[HttpPost("validate")]
public IActionResult ValidateCnpj([FromBody] CnpjRequest request)
{
    var validator = new CnpjValidator();
    var result = validator.Validate(request.Cnpj);

    if (result.IsValid)
    {
        return Ok(new
        {
            isValid = true,
            format = result.Format.ToString(),
            normalizedCnpj = result.NormalizedCnpj
        });
    }

    return BadRequest(new
    {
        isValid = false,
        error = result.ErrorMessage
    });
}
```

### 4. Normalização para Armazenamento

```csharp
public string NormalizeCnpjForStorage(string cnpj)
{
    var validator = new CnpjValidator();
    var result = validator.Validate(cnpj);
    
    if (result.IsValid)
    {
        // Armazena sempre normalizado (sem formatação)
        return result.NormalizedCnpj;
    }
    
    throw new ArgumentException($"CNPJ inválido: {result.ErrorMessage}");
}
```

## 🧪 Testes

A biblioteca inclui uma suíte completa de testes. Para executar:

```bash
dotnet test
```

Os testes cobrem:
- Validação de CNPJ numérico (formato tradicional)
- Validação de CNPJ alfanumérico (novo formato)
- Cálculo de dígitos verificadores
- Identificação de formato
- Normalização
- Tratamento de erros
- Configurações customizadas

## 📦 Estrutura da Biblioteca

```
next-CNPJ/
├── Core/
│   ├── Domain/
│   │   ├── CnpjConfiguration.cs      # Configuração de validação
│   │   ├── CnpjFormat.cs              # Enum de formato
│   │   └── CnpjValidationResult.cs   # Resultado da validação
│   ├── Services/
│   │   ├── CnpjValidator.cs          # Implementação do validador
│   │   ├── ICnpjValidator.cs         # Interface do validador
│   │   ├── CnpjFormatIdentifier.cs   # Identificador de formato
│   │   └── ICnpjFormatIdentifier.cs  # Interface do identificador
│   └── Utilities/
│       ├── AsciiConverter.cs          # Conversão ASCII para cálculo
│       ├── CnpjNormalizer.cs          # Normalização de CNPJ
│       └── DigitVerifierCalculator.cs # Cálculo de dígitos verificadores
```

## 🔗 Referências

- NTC 2025.001 - Especificação técnica do novo formato de CNPJ
- ENCAT - Especificação de letras excluídas

## 📄 Licença

[Adicione informações de licença aqui]

## 🤝 Contribuindo

Contribuições são bem-vindas! Por favor, abra uma issue ou pull request.
