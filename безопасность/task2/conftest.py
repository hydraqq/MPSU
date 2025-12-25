import pytest
import storage

@pytest.fixture(autouse=True)
def cleanup():
    storage.clear_data()
    yield
    storage.clear_data()
