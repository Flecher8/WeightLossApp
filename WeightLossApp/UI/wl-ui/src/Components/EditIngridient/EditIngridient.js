import { useState } from "react";
import variables from "../../Variables";
import { Modal, Button, Form, Row, Col } from "react-bootstrap";

function EditIngridient(props) {
	const [validated, setValidated] = useState(false);

	const handleSubmit = event => {
		const form = event.currentTarget;
		if (!form.checkValidity()) {
			event.preventDefault();
			event.stopPropagation();
		} else {
			event.preventDefault();

			fetch(`${variables.API_URL}/IngridientData`, {
				method: "PUT",
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				},

				body: JSON.stringify(props.ingridient)
			})
				.then(res => res.json())
				.then(
					result => {
						props.getIngridients();
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
		<div className="EditIngridient">
			<Form noValidate validated={validated} onSubmit={handleSubmit}>
				<Modal.Header closeButton>
					<Modal.Title>Edit ingridient</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom01">
							<Form.Control
								required
								pattern="^[A-Z][a-z]*$"
								type="text"
								placeholder="Name"
								value={props.ingridient.Name}
								onChange={e =>
									props.setIngridient({
										ID: props.ingridient.ID,
										Name: e.target.value,
										Calories: props.ingridient.Calories,
										Proteins: props.ingridient.Proteins,
										Carbohydrates: props.ingridient.Carbohydrates,
										Fats: props.ingridient.Fats
									})
								}
							/>
						</Form.Group>
						<Form.Group as={Col} controlId="validationCustom02">
							<Form.Control
								required
								pattern="^[1-9][0-9]*$"
								type="text"
								placeholder="Calories"
								value={props.ingridient.Calories}
								onChange={e =>
									props.setIngridient({
										ID: props.ingridient.ID,
										Name: props.ingridient.Name,
										Calories: e.target.value,
										Proteins: props.ingridient.Proteins,
										Carbohydrates: props.ingridient.Carbohydrates,
										Fats: props.ingridient.Fats
									})
								}
							/>
						</Form.Group>
						<Form.Group as={Col} controlId="validationCustom03">
							<Form.Control
								required
								pattern="^[1-9][0-9]*$"
								type="text"
								placeholder="Proteins"
								value={props.ingridient.Proteins}
								onChange={e =>
									props.setIngridient({
										ID: props.ingridient.ID,
										Name: props.ingridient.Name,
										Calories: props.ingridient.Calories,
										Proteins: e.target.value,
										Carbohydrates: props.ingridient.Carbohydrates,
										Fats: props.ingridient.Fats
									})
								}
							/>
						</Form.Group>
						<Form.Group as={Col} controlId="validationCustom04">
							<Form.Control
								required
								pattern="^[1-9][0-9]*$"
								type="text"
								placeholder="Carbohydrates"
								value={props.ingridient.Carbohydrates}
								onChange={e =>
									props.setIngridient({
										ID: props.ingridient.ID,
										Name: props.ingridient.Name,
										Calories: props.ingridient.Calories,
										Proteins: props.ingridient.Proteins,
										Carbohydrates: e.target.value,
										Fats: props.ingridient.Fats
									})
								}
							/>
						</Form.Group>
						<Form.Group as={Col} controlId="validationCustom05">
							<Form.Control
								required
								pattern="^[1-9][0-9]*$"
								type="text"
								placeholder="Fats"
								value={props.ingridient.Fats}
								onChange={e =>
									props.setIngridient({
										ID: props.ingridient.ID,
										Name: props.ingridient.Name,
										Calories: props.ingridient.Calories,
										Proteins: props.ingridient.Proteins,
										Carbohydrates: props.ingridient.Carbohydrates,
										Fats: e.target.value
									})
								}
							/>
						</Form.Group>
					</Row>
				</Modal.Body>
				<Modal.Footer>
					<Button variant="primary" type="submit">
						Save Changes
					</Button>
				</Modal.Footer>
			</Form>
		</div>
	);
}

export default EditIngridient;
