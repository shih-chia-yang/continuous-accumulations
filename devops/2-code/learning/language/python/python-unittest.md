# Python Unittest

[[python]]

[[pytest]]

[how to test a function that raises an exception](https://nickolaskraus.org/articles/how-to-test-a-function-that-raises-an-exception/)


```python
def set(self, k, v):
    """
    Set the value of a Setting specified by its key.

    :param k: key of Setting
    :type k: str
    :param v: value of Setting
    :type v: bool
    :return: None
    :rtype: None
    """
    if not isinstance(v, bool):
        raise TypeError(
            '\'{}\' is not of type bool.'.format(v))

    for setting in self.settings:
        if setting.key == k:
            setting.value = v
            return
    else:
        raise KeyError(k)
```

- use `assertRaises`
There are two ways to use assertRaises:
Using keyword arguments.
Using a context manager.


The first is the most straight forward:
```python
assertRaises(exception, callable, *args, **kwds)
```

The second is slightly more involved:

```python
assertRaises(exception, msg=None)
```

Here is a unit test that tests for both the KeyError and ValueError with its exception message raised in the original function:
```python
def test_set(self):
    m = MessageSettings(**self.kwargs)
    m.set('a', True)
    self.assertEqual(MessageSettings(
        settings=[
            Setting(
                key='a',
                name='b',
                value=True
            )]
    ), m)
    self.assertRaises(KeyError, m.set, **{'k': 'x', 'v': True})
    with self.assertRaises(TypeError) as context:
        m.set('a', 'True')
    self.assertEqual(
        '\'True\' is not of type bool.', str(context.exception))
```

I have seen instances where a side_effect is used in order to simulate an exception. For example:
```python
import unittest

import my_module


class MyTestCase(unittest.TestCase):
    def setUp(self):
        self.mock_logging = patch.object(
            my_module, 'logging', autoSpec=True).start()

    def test(self):
        self.mock_logging.info.side_effect = my_module.DeadlineExceededError
        ...
```

[//begin]: # "Autogenerated link references for markdown compatibility"
[python]: python.md "Python"
[pytest]: pytest.md "pytest"
[//end]: # "Autogenerated link references"