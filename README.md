# log4net.Filter.FuzzyStringFilter

Example configuration:
```
<log4net>
    <appender name="SMTPAppender" type="log4net.Appender.SmtpAppender,log4net">
      <bufferSize value="1"/>
      <authentication value="Basic" />
      <to value="to@example.com" />
      ...
      <filter type="log4net.Filter.FuzzyStringFilter,log4net.Filter.FuzzyStringFilter">
	
	<!-- Only send one fuzzy match email every 300 seconds.  Default is 300. -->
        <DecaySeconds value="300"/>
        
	<!-- Reject strings that are quite similar - 1.0 is an exact match, .33 is roughly similar.  Default is 0.8. -->
        <MinimumDiceCoefficient value=".80"/>
	
      </filter>
      ...
    </appender>
    <root>
      <level value="ALL" />
      <appender-ref ref="SMTPAppender" />
    </root>
</log4net>
```
