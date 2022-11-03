# Contribution Guide

Everyone is allowed to contribute to this repository - all contribution is welcome.

## Reporting Issues

If you don't have any coding skills, a perfect way to contribute is sending detailed reports when you encounter an issue in the software. Please make sure to include detailed steps for reproduction. The better a report is described, the faster it can be resolved.

## Getting started 

You are welcome to take part in the active development. Do the following steps to work on it:
1. Fork the repository to your own GitHub account
2. Do your code changes
3. Maybe create some tests
4. Create the Pull Request to this repository

## Claim Syncfusion License

To make the software build-able on your device, you need to claim a license for the Syncfusion products. For this, you can use the free Community License
1. Go to [Syncfusion website](https://www.syncfusion.com/products/communitylicense) and press the "CLAIM FREE LICENSE" button
2. Sign in with your LinkedIn or XING account
3. Navigate to [Downloads & Keys](https://www.syncfusion.com/account/downloads)
4. Click "Get License Key"
5. In the popup, select "WPF" as platform and "20.3460.0.52" as version and press "GET LICENSE KEY"
6. Go to your Environment Variables and add a variable named "SyncFusion_20.3460.0.52_License"
7. As value, configure the License Key you received after **Step 5**

## Adding NuGet packages

When adding a NuGet package, make sure it is compatible with the MIT license of this project. Add the corresponding entry to the [Third Party Licenses](source/PdfCombiner/ViewModel/ThirdPartyLicensesViewModel.cs).