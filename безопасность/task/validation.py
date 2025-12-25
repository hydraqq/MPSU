from dataclasses import dataclass, field
import re


ERR_LENGTH = "length"
ERR_LETTER = "requires_letter"
ERR_DIGIT = "requires_digit"
ERR_SPECIAL = "requires_special"


@dataclass
class PasswordValidationResult:
    is_valid: bool
    errors: list[str] = field(default_factory=list)
    warnings: list[str] = field(default_factory=list)

    def __bool__(self) -> bool:
        return self.is_valid


def validate_password(password: str) -> PasswordValidationResult:
    errors = []
    
    if len(password) < 12:
        errors.append(ERR_LENGTH)
    
    if not re.search(r'[a-zA-Z]', password):
        errors.append(ERR_LETTER)
    
    if not re.search(r'[0-9]', password):
        errors.append(ERR_DIGIT)
    
    if not re.search(r'[!@#$%^&*()_+\-=\[\]{};:\'",.<>?/\\|`~]', password):
        errors.append(ERR_SPECIAL)
    
    return PasswordValidationResult(is_valid=len(errors) == 0, errors=errors)
