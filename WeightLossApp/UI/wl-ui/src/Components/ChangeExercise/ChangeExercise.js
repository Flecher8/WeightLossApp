import { useState } from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col } from "react-bootstrap";

function ChangeExercise(props) {
	const [validated, setValidated] = useState(false);

	// Save img in imgur and write img to object
	function saveImg(ev) {
		const formdata = new FormData();
		formdata.append("image", ev.target.files[0]);
		fetch(constants.API_Imgur, {
			method: "post",
			headers: {
				Authorization: "Client-ID " + constants.Client_ID,
				Accept: "application/json"
			},
			body: formdata
		})
			.then(res => res.json())
			.then(data => {
				props.setExercise({
					ID: props.exercise.ID,
					Section: props.exercise.Section,
					Name: props.exercise.Name,
					Length: props.exercise.Length,
					Instructions: props.exercise.Instructions,
					ImageName: data.data.link,
					BurntCalories: props.exercise.BurntCalories,
					NumberOfReps: props.exercise.NumberOfReps
				});
			});
	}

	// Fetch function
	const handleSubmit = event => {
		const form = event.currentTarget;
		if (!form.checkValidity()) {
			event.preventDefault();
			event.stopPropagation();
		} else {
			event.preventDefault();
			fetch(constants.API_URL + "Exercise", {
				method: props.method,
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				},

				body: JSON.stringify(props.exercise)
			})
				.then(res => res.json())
				.then(
					result => {
						// Update main table
						props.getExercises();

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
		<div className="AddExercise">
			<Form noValidate validated={validated} onSubmit={handleSubmit}>
				<Modal.Header closeButton>
					<Modal.Title>{props.title}</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom01">
							<Form.Label className="ms-1">Section</Form.Label>
							<Form.Control
								required
								pattern="^[A-ZА-ЯЁ][a-zа-яё]*$"
								type="text"
								maxLength="50"
								placeholder="Section"
								value={props.exercise.Section}
								onChange={e =>
									props.setExercise({
										ID: props.exercise.ID,
										Section: e.target.value,
										Name: props.exercise.Name,
										Length: props.exercise.Length,
										Instructions: props.exercise.Instructions,
										ImageName: props.exercise.ImageName,
										BurntCalories: props.exercise.BurntCalories,
										NumberOfReps: props.exercise.NumberOfReps
									})
								}
							/>
						</Form.Group>
					</Row>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom02">
							<Form.Label className="ms-1">Name</Form.Label>
							<Form.Control
								required
								pattern="^[A-ZА-ЯЁ][a-zа-яё]*$"
								type="text"
								placeholder="Name"
								maxLength="50"
								value={props.exercise.Name}
								onChange={e =>
									props.setExercise({
										ID: props.exercise.ID,
										Section: props.exercise.Section,
										Name: e.target.value,
										Length: props.exercise.Length,
										Instructions: props.exercise.Instructions,
										ImageName: props.exercise.ImageName,
										BurntCalories: props.exercise.BurntCalories,
										NumberOfReps: props.exercise.NumberOfReps
									})
								}
							/>
						</Form.Group>
					</Row>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom03">
							<Form.Label className="ms-1">Length</Form.Label>
							<Form.Control
								required
								pattern="^[1-9][0-9]*$"
								type="text"
								placeholder="Length"
								value={props.exercise.Length}
								onChange={e =>
									props.setExercise({
										ID: props.exercise.ID,
										Section: props.exercise.Section,
										Name: props.exercise.Name,
										Length: e.target.value,
										Instructions: props.exercise.Instructions,
										ImageName: props.exercise.ImageName,
										BurntCalories: props.exercise.BurntCalories,
										NumberOfReps: props.exercise.NumberOfReps
									})
								}
							/>
						</Form.Group>
					</Row>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom04">
							<Form.Label className="ms-1">Instructions</Form.Label>
							<Form.Control
								required
								type="text"
								placeholder="Instructions"
								maxLength="250"
								value={props.exercise.Instructions}
								onChange={e =>
									props.setExercise({
										ID: props.exercise.ID,
										Section: props.exercise.Section,
										Name: props.exercise.Name,
										Length: props.exercise.Length,
										Instructions: e.target.value,
										ImageName: props.exercise.ImageName,
										BurntCalories: props.exercise.BurntCalories,
										NumberOfReps: props.exercise.NumberOfReps
									})
								}
							/>
						</Form.Group>
					</Row>
					<Row className="mb-3">
						{/* if the object is being edited - display an image */}
						{props.method === "PUT" && (
							<div className="container border mb-3">
								<p>Image now</p>
								<img src={props.exercise.ImageName} alt="unloaded img" width="150px" />
							</div>
						)}
						<Form.Group as={Col} controlId="validationCustom05">
							<Form.Label className="ms-1">Image</Form.Label>
							<Form.Control
								type="file"
								placeholder="ImageName"
								value={props.exercise.ImgName}
								onChange={e => saveImg(e)}
							/>
						</Form.Group>
					</Row>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom06">
							<Form.Label className="ms-1">BurntCalories</Form.Label>
							<Form.Control
								required
								pattern="^[1-9][0-9]*$"
								type="text"
								placeholder="BurntCalories"
								value={props.exercise.BurntCalories}
								onChange={e =>
									props.setExercise({
										ID: props.exercise.ID,
										Section: props.exercise.Section,
										Name: props.exercise.Name,
										Length: props.exercise.Length,
										Instructions: props.exercise.Instructions,
										ImageName: props.exercise.ImageName,
										BurntCalories: e.target.value,
										NumberOfReps: props.exercise.NumberOfReps
									})
								}
							/>
						</Form.Group>
					</Row>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom07">
							<Form.Label className="ms-1">NumberOfReps</Form.Label>
							<Form.Control
								required
								pattern="^[1-9][0-9]*$"
								type="text"
								placeholder="NumberOfReps"
								value={props.exercise.NumberOfReps}
								onChange={e =>
									props.setExercise({
										ID: props.exercise.ID,
										Section: props.exercise.Section,
										Name: props.exercise.Name,
										Length: props.exercise.Length,
										Instructions: props.exercise.Instructions,
										ImageName: props.exercise.ImageName,
										BurntCalories: props.exercise.BurntCalories,
										NumberOfReps: e.target.value
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

export default ChangeExercise;
