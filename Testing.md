# Calculator API Testing Documentation

## Overview
The Calculator API testing was implemented using a simple `.http` file, which provides a straightforward way to test HTTP endpoints. The tests utilize VS Code's REST Client or similar IDE extensions to execute HTTP requests and validate responses.

## Test Setup
- File: [CalculatorAPI.http](Code/EnhancedProject/CalculatorAPI.http)
- Base URL: Configured using a variable `{{url}}` which can be change based on the environments 
    - Local: `http://localhost:5242`
    - Cloud: `https://calculator.drilind.com`
- Content Type: All requests use `application/json`

## Test Scenarios

### Addition Endpoint (`/add`)

1. **Valid Number Addition**
   - Input: `number1: 5, number2: 3`
   - Expected: Status 200, result = 8
   - Validates: Basic addition functionality

2. **Negative Number Addition**
   - Input: `number1: -5, number2: 3`
   - Expected: Status 200, result = -2
   - Validates: Handling of negative numbers

3. **Missing Parameter**
   - Input: Only `number1: 5` provided
   - Expected: Status 400, error message "Number2 is required"
   - Validates: Required field validation

4. **Invalid Input**
   - Input: `number1: "abc", number2: 3`
   - Expected: Status 400
   - Validates: Type validation

### Subtraction Endpoint (`/subtract`)

1. **Valid Number Subtraction**
   - Input: `number1: 10, number2: 3`
   - Expected: Status 200, result = 7
   - Validates: Basic subtraction functionality

2. **Negative Number Subtraction**
   - Input: `number1: -10, number2: 3`
   - Expected: Status 200, result = -13
   - Validates: Handling of negative numbers

3. **Missing Parameter**
   - Input: Only `number2: 3` provided
   - Expected: Status 400, error message "Number1 is required"
   - Validates: Required field validation

4. **Invalid Input**
   - Input: `number1: "abc", number2: 3`
   - Expected: Status 400
   - Validates: Type validation

## Test Assertions
Each test includes assertions to verify:
- Correct HTTP status codes (200 for success, 400 for errors)
- Expected response format (JSON)
- Correct calculation results
- Appropriate error messages for validation failures

## Benefits of This Approach
1. Simple and readable test format
2. Easy to maintain and version control
3. Quick to execute and debug
4. No additional testing framework required
5. Can be run directly from the IDE
6. Serves as both testing and API documentation

## Running the Tests
Tests can be executed individually or all at once using VS Code's REST Client extension or similar tools in other IDEs. Each request is separated by `###` markers and includes its own assertions.