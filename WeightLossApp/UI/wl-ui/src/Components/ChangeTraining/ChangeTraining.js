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

	useEffect(() => {
		if (props.training.TrainingExercise[props.training.TrainingExercise.length - 1] == "Add") {
			processTraining(props.training);
		}
	}, [props.training]);

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

	// Get selected options in Multiselect component of the form
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

	function getTrainingId() {
		return props.training.Id === undefined ? 1 : props.training.Id;
	}

	// Set rows in table TrainingsExercise
	function setTrainingExerciseInTraining() {
		let trainingId = getTrainingId();
		let arr = [];
		for (let item of selected) {
			arr.push({
				TrainingId: trainingId,
				ExerciseId: Number(item.value)
			});
		}
		arr.push("Add");
		let object = {
			Id: props.training.Id,
			SectionTrainingId: props.training.SectionTrainingId,
			Complexity: props.training.Complexity,
			TrainingExercise: arr
		};

		return object;
	}

	//
	function processTraining(param) {
		// Get rid of 'Add' at the end of the array
		param.TrainingExercise.pop();

		fetch(constants.API_URL + "Training", {
			method: props.method,
			headers: {
				Accept: "application/json",
				"Content-Type": "application/json"
			},

			body: JSON.stringify(param)
		})
			.then(res => res.json())
			.then(
				result => {
					// Update main table
					props.getTrainings();
					// Close
					props.state();
				},
				error => {
					alert("Failed");
				}
			);

		if (param.Id != undefined) {
			if (param.TrainingExercise.length === 0) {
				fetch(constants.API_URL + `Training/TrainingExercise/${param.Id}`, {
					method: "DELETE",
					headers: {
						Accept: "application/json",
						"Content-Type": "application/json"
					}
				})
					.then(res => res.json())
					.then(
						result => {
							// Update main table
							props.getTrainings();
							// Close
							props.state();
						},
						error => {
							alert("Failed");
						}
					);
			} else {
				fetch(constants.API_URL + `Training/TrainingExercise`, {
					method: "POST",
					headers: {
						Accept: "application/json",
						"Content-Type": "application/json"
					},

					body: JSON.stringify(param.TrainingExercise)
				})
					.then(res => res.json())
					.then(
						result => {
							// Update main table
							props.getTrainings();
							// Close
							props.state();
						},
						error => {
							alert("Failed");
						}
					);
			}
		}
	}

	// Fetch function
	const handleSubmit = event => {
		const form = event.currentTarget;
		if (!form.checkValidity()) {
			event.preventDefault();
			event.stopPropagation();
		} else {
			event.preventDefault();
			props.setTraining(setTrainingExerciseInTraining());
		}

		setValidated(true);
	};

	// Get section name function
	function getSectionType(id) {
		for (let item of props.sectionTraining) {
			if (item.Id === id) {
				return item.Type;
			}
		}
		return "NO SECTION TYPE";
	}

	return (
		<div className="ChangeAchievement">
			<Form
				noValidate
				validated={validated}
				onSubmit={e => {
					handleSubmit(e);
				}}>
				<Modal.Header closeButton>
					<Modal.Title>{props.title}</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom01">
							<Form.Label className="ms-1">SectionId</Form.Label>
							<Form.Select
								onChange={e =>
									props.setTraining({
										Id: props.training.Id,
										SectionTrainingId: Number(e.target.value),
										Complexity: props.training.Complexity,
										TrainingExercise: props.training.TrainingExercise
									})
								}>
								<option key={0} value={props.training.SectionTrainingId}>
									Default {getSectionType(props.training.SectionTrainingId)}
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
										SectionTrainingId: props.training.SectionTrainingId,
										Complexity: e.target.value,
										TrainingExercise: props.training.TrainingExercise
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
