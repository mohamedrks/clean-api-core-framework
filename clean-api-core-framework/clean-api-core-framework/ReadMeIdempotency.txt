1. Generate the idempotency key from front end , if you create it from back end it becomes useless.
2. When you send the request from front-end you should store the impodency key in the local storage or session storage.
	if you are retrying then you can resue the same idempotency key.
3. When you create idempotency key try to make it unique by hashing the request body and the request url or userid.
4. You can also set an expiration time for the idempotency keys to prevent them from being stored indefinitely.
5. This can be done by using a background job to periodically clean up old idempotency keys from the database.