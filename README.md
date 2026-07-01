<a href="https://www.buymeacoffee.com/almerindo" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a>

# next-CNPJ

.NET library for robust CNPJ validation with support for the new alphanumeric format according to NTC 2025.001.

## 📋 About

The **next-CNPJ** library provides complete CNPJ (Cadastro Nacional da Pessoa Jurídica) validation, including support for the new alphanumeric format that will come into effect. It implements all NTC 2025.001 rules, including calculation and validation of check digits, automatic normalization, and format identification.

## ✨ Features

- ✅ Numeric CNPJ validation (traditional format)
- ✅ Alphanumeric CNPJ validation (new NTC 2025.001 format)
- ✅ Automatic check digit calculation and validation
- ✅ Automatic normalization (removes formatting: dots, slashes, hyphens)
- ✅ Automatic format identification (numeric or alphanumeric)
- ✅ Excluded letters configuration (I, O, U, Q, F according to ENCAT)
- ✅ Support for CNPJ with or without formatting
- ✅ Automatic lowercase to uppercase conversion
- ✅ Rejection of CNPJ with all identical characters (e.g., 00000000000000)
- ✅ Detailed results with descriptive error messages

## 🚀 Installation

Install the package via NuGet:

```bash
dotnet add package next-CNPJ
```

Or via Package Manager Console:

```powershell
Install-Package next-CNPJ
```

## 📖 Basic Usage

### Simple Validation

```csharp
using next_CNPJ.Core.Services;

var validator = new CnpjValidator();

// Simple validation - returns true/false
bool isValid = validator.IsValid("11222333000181");
Console.WriteLine($"Valid CNPJ: {isValid}"); // true
```

### Detailed Validation

```csharp
using next_CNPJ.Core.Services;
using next_CNPJ.Core.Domain;

var validator = new CnpjValidator();

// Detailed validation - returns complete information
var result = validator.Validate("11222333000181");

if (result.IsValid)
{
    Console.WriteLine($"Valid CNPJ!");
    Console.WriteLine($"Format: {result.Format}"); // Numeric
    Console.WriteLine($"Normalized CNPJ: {result.NormalizedCnpj}"); // 11222333000181
}
else
{
    Console.WriteLine($"Error: {result.ErrorMessage}");
}
```

### Validation with Formatting

The library accepts CNPJ with or without formatting:

```csharp
var validator = new CnpjValidator();

// All these forms are accepted:
validator.IsValid("11222333000181");           // Without formatting
validator.IsValid("11.222.333/0001-81");      // With traditional formatting
validator.IsValid("12.ABC.345/01DE-35");      // Alphanumeric format with formatting
```

## 🔤 Alphanumeric CNPJ

### Alphanumeric CNPJ Validation

The new alphanumeric format allows letters in the root (positions 1-8) or in the order (positions 9-12):

```csharp
var validator = new CnpjValidator();

// Example of valid alphanumeric CNPJ
var result = validator.Validate("12ABC34501DE35");

if (result.IsValid)
{
    Console.WriteLine($"Format: {result.Format}"); // Alphanumeric
    Console.WriteLine($"Normalized CNPJ: {result.NormalizedCnpj}"); // 12ABC34501DE35
}
```

### Official Validator Behavior

The validator follows the current behavior of the official Receita Federal validator and accepts any uppercase letter (`A-Z`) or digit in the first 12 positions of an alphanumeric CNPJ:

```regex
[A-Z0-9]{12}[0-9]{2}
```

For example, the CNPJ below contains the letters `O` and is valid when its check digits match:

```csharp
var validator = new CnpjValidator();

var result = validator.Validate("0G3BYOOH000158");
Console.WriteLine(result.IsValid); // true
```

The letters I, O, U, Q, and F were mentioned in the NT as depending on Receita Federal confirmation. Because the official validator accepts them, the library does not reject those letters to avoid false negatives.


### Optional Technical Standard Letter Validation

If you need to enforce the historical NT restriction for the letters I, O, U, Q, and F, enable it explicitly through `CnpjConfiguration`:

```csharp
using next_CNPJ.Core.Domain;
using next_CNPJ.Core.Services;

var validator = new CnpjValidator();
var config = new CnpjConfiguration
{
    ValidateExcludedLetters = true
};

var result = validator.Validate("0G3BYOOH000158", config);
Console.WriteLine(result.IsValid); // false
```

This option is disabled by default so the library remains aligned with the official Receita Federal validator.

### Invalid CNPJ Patterns

The library rejects CNPJs with all identical characters (including all zeros):

```csharp
var validator = new CnpjValidator();

// CNPJ with all zeros - invalid
var result = validator.Validate("00000000000000");
Console.WriteLine(result.IsValid); // false
Console.WriteLine(result.ErrorMessage); // "CNPJ inválido: todos os caracteres são iguais."

// CNPJ with all same digits - invalid
var result2 = validator.Validate("11111111111111");
Console.WriteLine(result2.IsValid); // false
Console.WriteLine(result2.ErrorMessage); // "CNPJ inválido: todos os caracteres são iguais."
```

## 🔍 Format Identification

You can identify the CNPJ format before validating:

```csharp
using next_CNPJ.Core.Services;

var identifier = new CnpjFormatIdentifier();

// Identify format
var format = identifier.IdentifyFormat("12ABC34501DE35");
Console.WriteLine(format); // Alphanumeric

// Quick checks
bool isAlphanumeric = identifier.IsAlphanumeric("12ABC34501DE35"); // true
bool isNumeric = identifier.IsNumeric("11222333000181"); // true
```

## 📚 API Reference

### `ICnpjValidator`

Main interface for CNPJ validation.

#### Methods

- `bool IsValid(string? cnpj, CnpjConfiguration? config = null)`
  - Validates a CNPJ and returns `true` if valid, `false` otherwise.

- `CnpjValidationResult Validate(string? cnpj, CnpjConfiguration? config = null)`
  - Validates a CNPJ and returns a `CnpjValidationResult` object with detailed information.

### `CnpjValidationResult`

Validation result with the following properties:

- `bool IsValid` - Indicates if the CNPJ is valid
- `CnpjFormat Format` - Identified format (Numeric or Alphanumeric)
- `string? ErrorMessage` - Error message (null if valid)
- `string NormalizedCnpj` - Normalized CNPJ (without formatting)

### `ICnpjFormatIdentifier`

Interface for CNPJ format identification.

#### Methods

- `CnpjFormat IdentifyFormat(string? cnpj)` - Identifies the CNPJ format
- `bool IsAlphanumeric(string? cnpj)` - Checks if it is alphanumeric
- `bool IsNumeric(string? cnpj)` - Checks if it is numeric

### `CnpjConfiguration`

Configuration for custom validation.

#### Properties

- `char[] ExcludedLetters` - Letters that should not be accepted (default: I, O, U, Q, F)
- `bool AllowExcludedLetters` - Allows excluded letters even if they are in the list (default: false)

## 💡 Use Cases

### 1. Web Form Validation

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

### 2. Batch Processing

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

### 3. API Integration

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

### 4. Normalization for Storage

```csharp
public string NormalizeCnpjForStorage(string cnpj)
{
    var validator = new CnpjValidator();
    var result = validator.Validate(cnpj);
    
    if (result.IsValid)
    {
        // Always store normalized (without formatting)
        return result.NormalizedCnpj;
    }
    
    throw new ArgumentException($"Invalid CNPJ: {result.ErrorMessage}");
}
```

## 🧪 Testing

The library includes a complete test suite. To run:

```bash
dotnet test
```

Tests cover:
- Numeric CNPJ validation (traditional format)
- Alphanumeric CNPJ validation (new format)
- Check digit calculation
- Format identification
- Normalization
- Error handling (including rejection of all zeros and identical characters)
- Custom configurations

## 📦 Library Structure

```
next-CNPJ/
└── next-CNPJ/
    └── Core/
        ├── Domain/
        │   ├── CnpjConfiguration.cs      # Validation configuration
        │   ├── CnpjFormat.cs              # Format enum
        │   └── CnpjValidationResult.cs   # Validation result
        ├── Services/
        │   ├── CnpjValidator.cs          # Validator implementation
        │   ├── ICnpjValidator.cs         # Validator interface
        │   ├── CnpjFormatIdentifier.cs   # Format identifier
        │   └── ICnpjFormatIdentifier.cs  # Format identifier interface
        └── Utilities/
            ├── AsciiConverter.cs          # ASCII conversion for calculation
            ├── CnpjNormalizer.cs          # CNPJ normalization
            └── DigitVerifierCalculator.cs # Check digit calculation
```

## 🔗 References

- NTC 2025.001 - Technical specification for the new CNPJ format
- ENCAT - Excluded letters specification

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](../LICENSE) file for details.

## 🤝 Contributing

Contributions are welcome! Please open an issue or pull request.
