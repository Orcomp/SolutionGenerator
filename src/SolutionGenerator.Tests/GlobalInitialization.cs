// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalInitialization.cs" company="WildGums">
//   Copyright (c) 2013 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using ApprovalTests.Reporters;

#if DEBUG
[assembly: UseReporter(typeof (BeyondCompare3Reporter), typeof (DiffReporter), typeof (AllFailingTestsClipboardReporter))]
#endif