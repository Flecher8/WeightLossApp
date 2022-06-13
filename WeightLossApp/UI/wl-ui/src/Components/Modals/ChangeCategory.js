import { useState } from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col } from "react-bootstrap";

function ChangeCategory(props) {
	const [validated, setValidated] = useState(false);

	// Fetch function
	const handleSubmit = event => {
        console.log("asdas");
		const form = event.currentTarget;
		if (!form.checkValidity()) {
			event.preventDefault();
			event.stopPropagation();
            console.log("asdas2");
		} else {
			event.preventDefault();
			fetch(constants.API_URL + "Category", {
				method: props.method,
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				},

				body: JSON.stringify(props.category)
			})
				.then(res => res.json())
				.then( result => {
						// Update main table
						props.getCategories();

						// Close
						props.state();
					},
					error => {
						alert("Failed");
					}
				);
		}

		setValidated(true);
	};

	return (
		<div className="ChangeCategory">
			<Form noValidate validated={validated} onSubmit={handleSubmit}>
				<Modal.Header closeButton>
					<Modal.Title>{props.title}</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					<Row className="mb-3">
						<Form.Group as={Col}>
							<Form.Label className="ms-1">Name</Form.Label>
							<Form.Control
								required
								pattern="^.+$"
								type="text"
								maxLength="100"
								placeholder="Name"
								value={props.category.Name || ''}
								onChange={e => props.setCategory({ 
                                    Id: props.category.Id,
                                    Name: e.target.value,
                                    Type: props.category.Type,
                                    Danger: props.category.Danger 
                                })}
							/>
						</Form.Group>
					</Row>
					<Row className="mb-3">
						<Form.Group as={Col}>
							<Form.Label className="ms-1">Type</Form.Label>
							<Form.Control
								required
								pattern="^.+$"
								type="text"
								maxLength="100"
								placeholder="Type"
								value={props.category.Type || ''}
								onChange={e => props.setCategory({ 
                                    Type: e.target.value,
                                    Id: props.category.Id,
                                    Name: props.category.Name,
                                    Danger: props.category.Danger 
                                })}
							/>
						</Form.Group>
					</Row>
					<Row className="mb-3">
						<Form.Group as={Col}>
							<Form.Label className="ms-1">Danger</Form.Label>
							<Form.Control
								required
								pattern="^[0-5]$"
								type="text"
								placeholder="Danger"
								value={props.category.Danger || 0}
								onChange={e => props.setCategory({ 
                                    Danger: e.target.value,
                                    Type: props.category.Type,
                                    Id: props.category.Id,
                                    Name: props.category.Name,
                                })}
							/>
						</Form.Group>
					</Row>
				</Modal.Body>
				<Modal.Footer>
					<Button variant="primary" type="submit" className="btn btn-dark m-2 float-end">
						{props.title}
					</Button>
				</Modal.Footer>
			</Form>
		</div>
	);
}

export default ChangeCategory;
