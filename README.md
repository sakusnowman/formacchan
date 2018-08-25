# formacchan
Name
Formacchan

Overview
Formacchan is a format helper tool. This help to replace key to value, string.format by c#, and calculate formula.

## Description
When you use, you need to prepare Formatable file, KeyValuePairs File.
And execute it.
App find keys from Formatable file test, then replace from key to value.
You can use string.format of c# method, and caluculate formula.
Please look demo for more details.

## Demo
Formatable File:
I am {NAME}. My weight is <f=|{WEIGHT}<=>{0:#.0}|=f>kg : <f=|<c=|{WEIGHT}/0.453|=c><=>{0:#.00}|=f>lb.

Format Key Value File:
{NAME}<=>Jhon
{WEIGHT}<=>65

Result File:
I am Jhon. My weight is 65.0kg : 143.49lb.


Notes:
<f=| is Mark to use Format. <f=|VALUE<=>FORMATTER|=>
<c=| is Mark to calculate formula. <c=|FORMULA|=>
## Requirement
This use original IocContainer.
If you want use current version, download from my repository.

## Usage
dotnet formacchan.dll "formatable file path" "Key Value pair file path" "result file path"


## Licence

[MIT](https://github.com/tcnksm/tool/blob/master/LICENCE)

## Author

[sakusnowman](https://github.com/sakusnowman)