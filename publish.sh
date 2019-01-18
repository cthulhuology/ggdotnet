#!/bin/bash
dotnet publish -r linux-x64 -o /greengrass/ggc/deployment/lambda/arn.aws.lambda.eu-west-1.097591811552.function.ggdotnet.1
chown -R ggc_user  /greengrass/ggc/deployment/lambda/arn.aws.lambda.eu-west-1.097591811552.function.ggdotnet.1
