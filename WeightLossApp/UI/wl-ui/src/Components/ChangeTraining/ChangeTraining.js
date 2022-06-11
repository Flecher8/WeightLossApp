import { useState, useEffect } from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col } from "react-bootstrap";
import { MultiSelect } from "react-multi-select-component";

function ChangeTraining(props) {
	const [validated, setValidated] = useState(false);
	const [options, setOptions] = useState([]);
	const [selected, setSelected] = useState([]);

	// onLoad function
	useEffect(() => {
		setMultipleOptions();
		setMultipleSelected();
	}, []);

	// Set in useState options of exercises
	function setMultipleOptions() {
		let arr = [];
		props.exercises.forEach(e =>
			arr.push({
				label: e.Name,
				value: e.Id
			})
		);
		setOptions(arr);
	}

	function getExerciseName(id) {
		for (let exercise of props.exercises) {
			if (exercise.Id == id) return exercise.Name;
		}
		return "Nothing";
	}

	function setMultipleSelected() {
		let arr = [];
		props.training.TrainingExercise.forEach(e =>
			arr.push({
				label: getExerciseName(e.ExerciseId),
				value: e.ExerciseId
			})
		);
		setSelected(arr);
	}

	// Fetch function
	const handleSubmit = event => {
		const form = event.currentTarget;
		if (!form.checkValidity()) {
			event.preventDefault();
			event.stopPropagation();
		} else {
			event.preventDefault();
			// fetch(constants.API_URL + "Training", {
			// 	method: props.method,
			// 	headers: {
			// 		Accept: "application/json",
			// 		"Content-Type": "application/json"
			// 	},

			// 	body: JSON.stringify(props.training)
			// })
			// 	.then(res => res.json())
			// 	.then(
			// 		result => {
			// 			// Update main table
			// 			props.getTrainings();

			// 			// Close
			// 			props.state();
			// 		},
			// 		error => {
			// 			alert("Failed");
			// 		}
			// 	);
			console.log(selected);
		}

		setValidated(true);
	};

	return (
		<div className="ChangeAchievement">
			<Form noValidate validated={validated} onSubmit={handleSubmit}>
				<Modal.Header closeButton>
					<Modal.Title>{props.title}</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom01">
							<Form.Label className="ms-1">Section</Form.Label>
							<Form.Select
								onChange={e =>
									props.setTraining({
										Id: props.training.Id,
										Section: e.target.value,
										Complexity: props.training.Complexity
									})
								}>
								<option key={0} value={props.Section}>
									Default {props.Section}
								</option>
								{props.sectionTraining.map(section => (
									<option key={section.Id} value={section.Id}>
										{section.Type}
									</option>
								))}
							</Form.Select>
						</Form.Group>
					</Row>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom02">
							<Form.Label className="ms-1">Complexity</Form.Label>
							<Form.Control
								required
								pattern="^[A-ZА-ЯЁ][a-zа-яё]*$"
								type="text"
								maxLength="150"
								placeholder="Complexity"
								value={props.training.Complexity}
								onChange={e =>
									props.setTraining({
										Id: props.training.Id,
										Section: props.training.Section,
										Complexity: e.target.value
									})
								}
							/>
						</Form.Group>
					</Row>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom01">
							<Form.Label className="ms-1">Exercises</Form.Label>
							<MultiSelect options={options} value={selected} onChange={setSelected} labelledBy="Select" />
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

export default ChangeTraining;
