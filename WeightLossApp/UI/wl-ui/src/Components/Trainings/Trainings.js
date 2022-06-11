import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import {
	Modal,
	Button,
	Form,
	Row,
	Col,
	Table,
	InputGroup,
	FormControl,
	DropdownButton,
	Dropdown
} from "react-bootstrap";
import ChangeTraining from "../ChangeTraining/ChangeTraining";

function Trainings() {
	// Trainings data
	const [trainings, setTrainings] = useState([]);
	const [training, setTraining] = useState();

	// SectionTraining data
	const [sectionTraining, setSectionTraining] = useState([]);

	// Exercises data
	const [exercises, setExercises] = useState([]);
	const [exercisesDrop, setExercisesDrop] = useState([]);

	const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

	const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);

	// Get trainings data
	function getTrainings() {
		fetch(constants.API_URL + "Training")
			.then(res => res.json())
			.then(data => setTrainings(data));
	}

	// Get SectionTraining data
	function getSectionTraining() {
		fetch(constants.API_URL + "SectionTraining")
			.then(res => res.json())
			.then(data => setSectionTraining(data));
	}

	// Get exercises data
	function getExercises() {
		fetch(constants.API_URL + "Exercise")
			.then(res => res.json())
			.then(data => setExercises(data));
	}

	// Get exercises drop info
	function getExercisesDrop() {
		fetch(constants.API_URL + "Training/Exercise")
			.then(res => res.json())
			.then(data => setExercisesDrop(data));
	}

	// onLoad function
	useEffect(() => {
		getTrainings();
		getSectionTraining();
		getExercises();
		getExercisesDrop();
	}, []);

	// Show add modal function
	function addModalShow() {
		addHandleShow();
		setTraining({
			Id: undefined,
			SectionTrainingId: null,
			Complexity: "",
			TrainingExercise: []
		});
	}

	// Show edit modal function
	function editModalShow(training) {
		editHandleShow();
		setTraining(training);
	}

	// Delete item function
	function deleteClick(id) {
		if (window.confirm("Are you sure?")) {
			fetch(constants.API_URL + `Training/${id}`, {
				method: "DELETE",
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				}
			})
				.then(res => res.json())
				.then(
					result => {
						getTrainings();
					},
					error => {
						alert("Failed");
					}
				);
		}
	}

	// Get section name function
	function getSectionType(id) {
		for (let item of sectionTraining) {
			if (item.Id === id) {
				return item.Type;
			}
		}
		return "NO SECTION TYPE";
	}

	return (
		<div className="Trainings">
			{/* Create new item button */}
			<div className="container mt-5" align="right">
				<Button onClick={() => addModalShow()} variant="outline-primary">
					Create new training
				</Button>
			</div>
			{/* Create new item modal */}
			<Modal size="lg" centered show={addShow} onHide={addHandleClose}>
				<ChangeTraining
					state={addHandleClose}
					training={training}
					trainings={trainings}
					setTraining={setTraining}
					getTrainings={getTrainings}
					sectionTraining={sectionTraining}
					exercises={exercises}
					method="POST"
					title="Add new training"
				/>
			</Modal>
			{/* Main table */}
			<div className="container mt-5">
				<Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
					<thead>
						<tr>
							<th></th>
							<th>
								ID
								<Button className="btn-light">
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								Section
								<Button className="btn-light">
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								Complexity
								<Button className="btn-light">
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th></th>
						</tr>
					</thead>
					{trainings.map(e => (
						<tbody key={e.Id}>
							<tr>
								<td>
									{/* Dropdown menu with exercises */}
									<DropdownButton title="Exercises" id="dropdown-menu">
										{e.TrainingExercise.map(a => (
											<div key={a.ExerciseId}>
												<Dropdown.Item eventKey="option-1">
													{"Section: " + exercisesDrop[a.ExerciseId].Section}
												</Dropdown.Item>
												<Dropdown.Item eventKey="option-1">
													{"Name: " + exercisesDrop[a.ExerciseId].Name}
												</Dropdown.Item>
												<Dropdown.Item eventKey="option-1">
													{"Length: " + exercisesDrop[a.ExerciseId].Length}
												</Dropdown.Item>
												<Dropdown.Item eventKey="option-1">
													{"BurntCalories: " + exercisesDrop[a.ExerciseId].BurntCalories}
												</Dropdown.Item>
												<Dropdown.Item eventKey="option-1">
													{"NumberOfReps: " + exercisesDrop[a.ExerciseId].NumberOfReps}
												</Dropdown.Item>
												<Dropdown.Divider />
											</div>
										))}
									</DropdownButton>
								</td>
								<td>{e.Id}</td>
								<td>{getSectionType(e.SectionTrainingId)}</td>
								<td>{e.Complexity}</td>
								<td>
									<Button onClick={() => editModalShow(e)} variant="outline-dark">
										Edit
									</Button>
									<Button onClick={() => deleteClick(e.Id)} variant="outline-danger">
										Delete
									</Button>
								</td>
							</tr>
						</tbody>
					))}
				</Table>
			</div>
			{/* Edit item modal */}
			<Modal size="lg" centered show={editShow} onHide={editHandleClose}>
				<ChangeTraining
					state={editHandleClose}
					training={training}
					trainings={trainings}
					setTraining={setTraining}
					getTrainings={getTrainings}
					sectionTraining={sectionTraining}
					exercises={exercises}
					method="PUT"
					title="Edit training"
				/>
			</Modal>
		</div>
	);
}

export default Trainings;
