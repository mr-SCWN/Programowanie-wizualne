document.addEventListener('DOMContentLoaded', () => {
    const apiBaseUrl = window.apiBaseUrl || 'http://localhost:5000';

    const addProducerButton = document.getElementById('addProducerButton');
    const addProducerModal = document.getElementById('addProducerModal');
    const closeAddProducerModal = document.getElementById('closeAddProducerModal');
    const saveNewProducerButton = document.getElementById('saveNewProducerButton');
    const newProducerNameInput = document.getElementById('newProducerName');
    const editProducerModal = document.getElementById('editProducerModal');
    const closeEditProducerModal = document.getElementById('closeEditProducerModal');
    const saveEditProducerButton = document.getElementById('saveEditProducerButton');
    const editProducerNameInput = document.getElementById('editProducerName');
    const editProducerIdInput = document.getElementById('editProducerId');

    const addProductButton = document.getElementById('addProductButton');
    const addProductModal = document.getElementById('addProductModal');
    const closeAddProductModal = document.getElementById('closeAddProductModal');
    const saveNewProductButton = document.getElementById('saveNewProductButton');
    const newProductNameInput = document.getElementById('newProductName');
    const producerSelect = document.getElementById('producerSelect');
    const editProductModal = document.getElementById('editProductModal');
    const closeEditProductModal = document.getElementById('closeEditProductModal');
    const saveEditProductButton = document.getElementById('saveEditProductButton');
    const editProductNameInput = document.getElementById('editProductName');
    const editProductIdInput = document.getElementById('editProductId');
    const editProductProducer = document.getElementById('editProductProducer');

    addProducerButton.addEventListener('click', () => {
        newProducerNameInput.value = ''; 
        addProducerModal.style.display = 'block';
    });


    closeAddProducerModal.addEventListener('click', () => {
        addProducerModal.style.display = 'none';
    });


    closeEditProducerModal.addEventListener('click', () => {
        editProducerModal.style.display = 'none';
    });


    saveNewProducerButton.addEventListener('click', () => {
        const producerName = newProducerNameInput.value.trim();
        if (!producerName) {
            alert('Please enter producer name.');
            return;
        }

        fetch(`${apiBaseUrl}/api/producers`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ name: producerName })
        })
        .then(response => {
            if (response.ok) {
                location.reload();
            } else {
                alert('Error adding producer.');
            }
        });
    });


    document.querySelectorAll('.edit-producer-button').forEach(button => {
        button.addEventListener('click', () => {
            const producerId = button.dataset.producerId;
            const producerName = document.querySelector(`#producerRow-${producerId} td:first-child`).textContent;

            editProducerIdInput.value = producerId;
            editProducerNameInput.value = producerName;
            editProducerModal.style.display = 'block';
        });
    });


    saveEditProducerButton.addEventListener('click', () => {
        const producerId = editProducerIdInput.value;
        const producerName = editProducerNameInput.value.trim();

        if (!producerName) {
            alert('Please enter producer name.');
            return;
        }

        fetch(`${apiBaseUrl}/api/producers/${producerId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ id: parseInt(producerId, 10), name: producerName })
        })
        .then(response => {
            if (response.ok) {
                location.reload();
            } else {
                alert('Error updating producer.');
            }
        });
    });


    document.querySelectorAll('.delete-producer-button').forEach(button => {
        button.addEventListener('click', () => {
            const producerId = button.dataset.producerId;

            if (!confirm('Are you sure you want to delete this producer?')) {
                return;
            }

            fetch(`${apiBaseUrl}/api/producers/${producerId}`, {
                method: 'DELETE'
            })
            .then(response => {
                if (response.ok) {
                    location.reload();
                } else {
                    alert('Error deleting producer.');
                }
            });
        });
    });


    addProductButton.addEventListener('click', () => {
        newProductNameInput.value = ''; 
        producerSelect.value = ''; 
        addProductModal.style.display = 'block';
    });


    closeAddProductModal.addEventListener('click', () => {
        addProductModal.style.display = 'none';
    });


    saveNewProductButton.addEventListener('click', () => {
        const productName = newProductNameInput.value.trim();
        const producerId = producerSelect.value;

        if (!productName || !producerId) {
            alert('Please enter product name and select a producer.');
            return;
        }

        fetch(`${apiBaseUrl}/api/products`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                name: productName,
                producerId: parseInt(producerId, 10)
            })
        })
        .then(response => {
            if (response.ok) {
                location.reload();
            } else {
                alert('Error adding product.');
            }
        });
    });


    document.querySelectorAll('.edit-button').forEach(button => {
        button.addEventListener('click', () => {
            const productId = button.dataset.productId;

            fetch(`${apiBaseUrl}/api/products/${productId}`)
                .then(response => response.json())
                .then(product => {
                    if (product) {
                        document.getElementById('editProductId').value = product.id;
                        document.getElementById('editProductName').value = product.name;
                        document.getElementById('editProductProducer').value = product.producerId;
                        editProductModal.style.display = 'block';
                    } else {
                        alert('Product not found.');
                    }
                })
                .catch(() => alert('Error loading product data.'));
        });
    });


    closeEditProductModal.addEventListener('click', () => {
        editProductModal.style.display = 'none';
    });

 
    saveEditProductButton.addEventListener('click', () => {
        const productId = editProductIdInput.value;
        const productName = editProductNameInput.value.trim();
        const producerId = editProductProducer.value;

        if (!productName || !producerId) {
            alert('Please enter product name and select a producer.');
            return;
        }

        fetch(`${apiBaseUrl}/api/products/${productId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                id: parseInt(productId, 10),
                name: productName,
                producerId: parseInt(producerId, 10)
            })
        })
        .then(response => {
            if (response.ok) {
                location.reload();
            } else {
                alert('Error updating product.');
            }
        })
        .catch(() => alert('Error sending data.'));
    });


    document.querySelectorAll('.delete-button').forEach(button => {
        button.addEventListener('click', () => {
            const productId = button.dataset.productId;

            if (!confirm('Are you sure you want to delete this product?')) {
                return;
            }

            fetch(`${apiBaseUrl}/api/products/${productId}`, {
                method: 'DELETE'
            })
            .then(response => {
                if (response.ok) {
                    location.reload();
                } else {
                    alert('Error deleting product.');
                }
            });
        });
    });
});
