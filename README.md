# log4net.Filter.FuzzyStringFilter

Example configuration:
111
<log4net>
    <appender name="SMTPAppender" type="log4net.Appender.SmtpAppender,log4net">
      <bufferSize value="1"/>
      <authentication value="Basic" />
      <to value="to@example.com" />
      ...
      <filter type="log4net.Filter.FuzzyStringFilter,log4net.Filter.FuzzyStringFilter">
	      <!-- Only send one fuzzy match email every 5 minutes -->
        <DecaySeconds value="300"/>
        <!-- Reject strings that are quite similar - 1.0 is an exact match, .33 is roughly similar. -->
        <MinimumDiceCoefficient value=".80"/>
      </filter>
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%property{log4net:HostName}%newline%date user:%aspnet-request{AUTH_USER} %-5level %logger %newline %message%newline%exception%newline%newline"/>
      </layout>
    </appender>
    <root>
      <level value="ALL" />
      <appender-ref ref="SMTPAppender" />
    </root>
</log4net>
111
