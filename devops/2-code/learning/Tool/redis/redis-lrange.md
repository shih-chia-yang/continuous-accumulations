# redis-lrange

returns the specified elements of the list stored at key. the offsets start and stop are zero-based indexes, with 0 being the first element of the list (the head of the list ),1 being the next element and so on.
these offsets can also be negative numbers indicating offsets starting at the end of the list. for example, -1 is the last element of the list ,-2 the penultimate , and so on.