import { useState } from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col } from "react-bootstrap";

function ChangeSection(props) {
	const [validated, setValidated] = useState(false);

	// Fetch function
	const handleSubmit = event => {
		const form = event.currentTarget;
		if (!form.checkValidity()) {
			event.preventDefault();
			event.stopPropagation();
		} else {
			event.preventDefault();
			fetch(constants.API_URL + "SectionTraining", {
				method: props.method,
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				},

				body: JSON.stringify(props.section)
			})
				.then(res => res.json())
				.then(
					result => {
						// Update main table
						props.getSectionTraining();

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
		<div className="ChangeSection">
			<Form noValidate validated={validated} onSubmit={handleSubmit}>
				<Modal.Header closeButton>
					<Modal.Title>{props.title}</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom01">
							<Form.Label className="ms-1">Type</Form.Label>
							<Form.Control
								required
								pattern="^[A-ZА-ЯЁ][a-zа-яё]*$"
								type="text"
								maxLength="50"
								placeholder="Type"
								value={props.section.Type}
								onChange={e =>
									props.setSection({
										Id: props.section.Id,
										Type: e.target.value
									})
								}
							/>
						</Form.Group>
					</Row>
				</Modal.Body>
				<Modal.Footer>
					<Button variant="primary" type="submit">
						{props.title}
					</Button>
				</Modal.Footer>
			</Form>
		</div>
	);
}

export default ChangeSection;
