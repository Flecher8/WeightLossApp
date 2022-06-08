import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";
import ChangeExercise from "../ChangeExercise/ChangeExercise";

function Exercises() {
	// Exercises data
	const [exercises, setExercises] = useState([]);
	const [exercise, setExercise] = useState();

	const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

	const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);

	const [search, setSearch] = useState("");
	const [filterParameters, setFilterParameters] = useState({
		Section: false,
		Name: false,
		Length: false,
		BurntCalories: false,
		NumberOfReps: false
	});

	// Get exercises data
	function getExercises() {
		fetch(constants.API_URL + "Exercise")
			.then(res => res.json())
			.then(data => setExercises(data));
	}

	// onLoad function
	useEffect(() => {
		getExercises();
	}, []);

	// Show add modal function
	function addModalShow() {
		addHandleShow();
		setExercise({
			ID: undefined,
			Section: "",
			Name: "",
			Length: 0,
			Instructions: "",
			ImageName: "",
			BurntCalories: 0,
			NumberOfReps: 0
		});
	}

	// Show edit modal function
	function editModalShow(exercise) {
		editHandleShow();
		setExercise(exercise);
	}

	// Delete item function
	function deleteClick(id) {
		if (window.confirm("Are you sure?")) {
			fetch(constants.API_URL + `Exercise/${id}`, {
				method: "DELETE",
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				}
			})
				.then(res => res.json())
				.then(
					result => {
						getExercises();
					},
					error => {
						alert("Failed");
					}
				);
		}
	}

	// Seact by name
	function searchName(name) {
		let arr = [];
		for (let i = 0; i < exercises.length; i++) {
			if (exercises[i].Name.match(name)) {
				arr.push(exercises[i]);
			}
		}
		setExercises(arr);
	}

	// Sort functions
	function sortBySection() {
		if (filterParameters.Section) {
			exercises.sort((a, b) => a.Section.localeCompare(b.Section));
			setFilterParameters({
				Section: false,
				Name: filterParameters.Name,
				Length: filterParameters.Length,
				BurntCalories: filterParameters.BurntCalories,
				NumberOfReps: filterParameters.NumberOfReps
			});
		} else {
			exercises.sort((b, a) => a.Section.localeCompare(b.Section));
			setFilterParameters({
				Section: true,
				Name: filterParameters.Name,
				Length: filterParameters.Length,
				BurntCalories: filterParameters.BurntCalories,
				NumberOfReps: filterParameters.NumberOfReps
			});
		}
	}
	function sortByName() {
		if (filterParameters.Name) {
			exercises.sort((a, b) => a.Name.localeCompare(b.Name));
			setFilterParameters({
				Section: filterParameters.Section,
				Name: false,
				Length: filterParameters.Length,
				BurntCalories: filterParameters.BurntCalories,
				NumberOfReps: filterParameters.NumberOfReps
			});
		} else {
			exercises.sort((b, a) => a.Name.localeCompare(b.Name));
			setFilterParameters({
				Section: filterParameters.Section,
				Name: true,
				Length: filterParameters.Length,
				BurntCalories: filterParameters.BurntCalories,
				NumberOfReps: filterParameters.NumberOfReps
			});
		}
	}
	function sortByLength() {
		if (filterParameters.Length) {
			exercises.sort((a, b) => (a.Length > b.Length ? 1 : b.Length > a.Length ? -1 : 0));
			setFilterParameters({
				Section: filterParameters.Section,
				Name: filterParameters.Name,
				Length: false,
				BurntCalories: filterParameters.BurntCalories,
				NumberOfReps: filterParameters.NumberOfReps
			});
		} else {
			exercises.sort((a, b) => (a.Length < b.Length ? 1 : b.Length < a.Length ? -1 : 0));
			setFilterParameters({
				Section: filterParameters.Section,
				Name: filterParameters.Name,
				Length: true,
				BurntCalories: filterParameters.BurntCalories,
				NumberOfReps: filterParameters.NumberOfReps
			});
		}
	}
	function sortByBurntCalories() {
		if (filterParameters.BurntCalories) {
			exercises.sort((a, b) => (a.BurntCalories > b.BurntCalories ? 1 : b.BurntCalories > a.BurntCalories ? -1 : 0));
			setFilterParameters({
				Section: filterParameters.Section,
				Name: filterParameters.Name,
				Length: filterParameters.Length,
				BurntCalories: false,
				NumberOfReps: filterParameters.NumberOfReps
			});
		} else {
			exercises.sort((a, b) => (a.BurntCalories < b.BurntCalories ? 1 : b.BurntCalories < a.BurntCalories ? -1 : 0));
			setFilterParameters({
				Section: filterParameters.Section,
				Name: filterParameters.Name,
				Length: filterParameters.Length,
				BurntCalories: true,
				NumberOfReps: filterParameters.NumberOfReps
			});
		}
	}
	function sortByNumberOfReps() {
		if (filterParameters.NumberOfReps) {
			exercises.sort((a, b) => (a.NumberOfReps > b.NumberOfReps ? 1 : b.NumberOfReps > a.NumberOfReps ? -1 : 0));
			setFilterParameters({
				Section: filterParameters.Section,
				Name: filterParameters.Name,
				Length: filterParameters.Length,
				BurntCalories: filterParameters.BurntCalories,
				NumberOfReps: false
			});
		} else {
			exercises.sort((a, b) => (a.NumberOfReps < b.NumberOfReps ? 1 : b.NumberOfReps < a.NumberOfReps ? -1 : 0));
			setFilterParameters({
				Section: filterParameters.Section,
				Name: filterParameters.Name,
				Length: filterParameters.Length,
				BurntCalories: filterParameters.BurntCalories,
				NumberOfReps: true
			});
		}
	}

	return (
		<div className="Exercises container">
			{/* Create new item button */}
			<div className="container mt-5" align="right">
				<Button onClick={() => addModalShow()} variant="outline-primary">
					Create new exercise
				</Button>
			</div>
			{/* Search */}
			<div className="container mt-5">
				<InputGroup className="mb-3">
					<FormControl
						aria-label="Default"
						placeholder="Search"
						value={search}
						onChange={e => setSearch(e.target.value)}
						aria-describedby="inputGroup-sizing-default"
					/>
					<Button onClick={() => searchName(search)}>Search</Button>
					<Button onClick={() => getExercises()}>Cancel</Button>
				</InputGroup>
			</div>
			{/* Create new item modal */}
			<Modal size="lg" centered show={addShow} onHide={addHandleClose}>
				<ChangeExercise
					state={addHandleClose}
					exercise={exercise}
					setExercise={setExercise}
					getExercises={getExercises}
					method="POST"
					title="Add new exercise"
				/>
			</Modal>
			{/* Main table */}
			<div className="container mt-5">
				<Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
					<thead>
						<tr>
							<th>
								Section
								<Button className="btn-light" onClick={() => sortBySection()}>
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								Name
								<Button className="btn-light" onClick={() => sortByName()}>
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								Length
								<Button className="btn-light" onClick={() => sortByLength()}>
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								Instructions
								<Button className="btn-light">
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								ImageName
								<Button className="btn-light">
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								BurntCalories
								<Button className="btn-light" onClick={() => sortByBurntCalories()}>
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								NumberOfReps
								<Button className="btn-light" onClick={() => sortByNumberOfReps()}>
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th></th>
						</tr>
					</thead>
					<tbody>
						{exercises.map(e => (
							<tr key={e.ID}>
								<td>{e.Section}</td>
								<td>{e.Name}</td>
								<td>{e.Length}</td>
								<td className="overflow-auto">{e.Instructions}</td>
								<td>
									<img src={e.ImageName} alt="img" width="100px" />
								</td>
								<td>{e.BurntCalories}</td>
								<td>{e.NumberOfReps}</td>
								<td>
									<Button onClick={() => editModalShow(e)} variant="outline-dark">
										Edit
									</Button>
									<Button onClick={() => deleteClick(e.ID)} variant="outline-danger">
										Delete
									</Button>
								</td>
							</tr>
						))}
					</tbody>
				</Table>
			</div>
			{/* Edit item modal */}
			<Modal size="lg" centered show={editShow} onHide={editHandleClose}>
				<ChangeExercise
					state={editHandleClose}
					exercise={exercise}
					setExercise={setExercise}
					getExercises={getExercises}
					method="PUT"
					title="Edit exercise"
				/>
			</Modal>
		</div>
	);
}

export default Exercises;
