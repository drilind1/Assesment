# Evaluating Responses

## [1st Response from ChatGPT 4o:](1.ChatGPT-4o/Response.md.md)

    Did not give detailed instruction on how to create the project it did not include the required project packages.

    It did not have a separate response model to include a safe typed model instead it used a dynamic object which in this case I would not suggest.

    Tha validation it is simple which is good, but I would rather use fluentValidation which is easy to add more validations and a lot of extra features are included.

    The error handling is not ideal, returning the exception message on the response is not ideal instead of that a generic error should be returned and the failure should be only logged as suggested by the comment.

    The ChatGPT model used .net 6.0 version which is quite outdated this does not mean that it is bad and it probably is because chatgpt model does not have the latest data, this is not particular bad but would prefer that the latest LTS version be used like .net 8 
    It suggests to use a controller which for this scenario would prefer using minimal apis.

    Over all this response is not good and has a lot room for improvement.

##  [2nd Response from ChatGPT 4o:](2.ChatGPT-4o/Response.md.md)

   It suggested to use `dotnet new webapi -n ArithmeticApi` which is a good step to start with, but this template also includes `WeatherForecast` example which the model does not suggest to delete which leaves more clutter.

   It uses minimal apis which is good but the response model is still dynamic so that is not good.

   The validation could be improved instead of only checking for nulls and being inside the request model could be moved

   Uses .net 6 outdated, would prefer that it used .net 8 since it is the latest LTS version

   Did not create an easy maintainable structure of the project. 
   
   Overall this response works but it has a lot of room for improvement

##  [3rd Response from Claude Sonnet 3.5 (New version):](3.Claude%203.5%20Sonnet%20(New)//Response.md)

    It uses the latest .Net Core version 8.0 so this is good.

    It uses minimal APIs but they are all in the Program.cs file

    It uses fluent validation for validating the request, but it does an unnecessary check if trying to parse to decimal even though the type is already decimal

    It separates the request and response models to different files which is better.

    It handles errors like the Overflow exception and it returns a detailed failed response.

    Overall this is better response than the ChatGPT with 4o model, but it misses a cleaner project structure which can make the project harder to maintain and it includes some invalid validation checks

## [4th response from Claude Sonnet 3.5 (New version):](4.Claude%203.5%20Sonnet%20(New)//Response.md)

    It has all the good things of 3rd response and it gives instructions of a lot better project structure it separates the models in directories this makes the project a lot better and easier to maintain.

    It also uses a simple error model instead of depending on `AddProblemDetails` injection from 

    The validator does not include unnecessary checks and it is simple

    The error handling on the api calls is the only thing that can be improved it had a small compile error where it was passing `ErrorResponse` as the second parameter which is not accepted.

    The generated Dockerfile is a lot more convenient as it is similar to .net documented docker example

    **Overall this is the best response from the other 3, it has a clearer project structure and it does not have a lot of unnecessary dependencies.**
