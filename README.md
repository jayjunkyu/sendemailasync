# SendEmailAsync
This is a console app demonstration on how to design an email sending application while not being interrupted by background tasks. In most applications, it is critical that a user not be interrupted or delayed simply because an email is being sent. Some of the constraints that were considered were:

- Concise function signature SendMessage(message or messages)
- Email sender, recipient, subject, and boy, and date stored with status of send attempt (to keep track of which emails to resend).
- Three attempts must be made to send a message.

Run ConsoleApp1 first and then send messages using ConsoleApp2. ConsoleApp1 constantly checks if there are any failed emails and resends them up to three times every 1 second. Failed emails are kept track of by grabbing failed emails using a simple SQL stored procedure. All email sending methods are ran asynchronously for better performance.
